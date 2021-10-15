using System;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Collections.Generic;

namespace SharpDL.Tiles
{
    public class TileLayerContent : LayerContent
    {
        private uint[] data;

        public IReadOnlyCollection<uint> Data { get { return new List<uint>(data).AsReadOnly(); } }

        public TileLayerContent(XmlNode tileLayerNode)
            : base(tileLayerNode)
        {
            Utilities.ThrowExceptionIfChildNodeIsNull(tileLayerNode, "tileLayerNode", AttributeNames.TileLayerAttributes.Data);

            XmlNode dataNode = tileLayerNode[AttributeNames.TileLayerAttributes.Data];
            data = new uint[Width * Height];

            if (dataNode.Attributes[AttributeNames.TileLayerAttributes.Encoding] != null)
            {
                Utilities.ThrowExceptionIfAttributeIsNull(dataNode, "dataNode", AttributeNames.TileLayerAttributes.Encoding);

                string encoding = dataNode.Attributes[AttributeNames.TileLayerAttributes.Encoding].Value;

                if (encoding == "base64")
                    ReadAsBase64(tileLayerNode, dataNode);
                else if (encoding == "csv")
                    ReadAsCSV(tileLayerNode);
                else
                    throw new InvalidOperationException(String.Format("Unsupported encoding type: {0}. Only 'base64' and 'csv' are supported at this time.", encoding));
            }
        }

        private void ReadAsBase64(XmlNode node, XmlNode dataNode)
        {
            if (String.IsNullOrEmpty(node.InnerText))
            {
                return;
            }

            Stream uncompressedData = new MemoryStream(Convert.FromBase64String(node.InnerText), false);

            // compression is optional, so we check if the attribute is available
            if (dataNode.Attributes[AttributeNames.TileLayerAttributes.Compression] != null)
            {
                string compression = dataNode.Attributes[AttributeNames.TileLayerAttributes.Compression].Value;

                if (compression == "gzip")
                    uncompressedData = new GZipStream(uncompressedData, CompressionMode.Decompress, false);
                else if (compression == "zlib")
                    uncompressedData = new Ionic.Zlib.ZlibStream(uncompressedData, Ionic.Zlib.CompressionMode.Decompress, false);
                else
                    throw new InvalidOperationException(String.Format("Unsupported compression type: {0}. Only 'gzip' and 'zlib' are supported at this time.", compression));
            }

            using (uncompressedData)
            {
                using (BinaryReader reader = new BinaryReader(uncompressedData))
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = reader.ReadUInt32();
                    }
                }
            }
        }

        private void ReadAsCSV(XmlNode node)
        {
            if(String.IsNullOrEmpty(node.InnerText))
            {
                return;
            }

            string[] lines = node.InnerText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] indices = lines[i].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int j = 0; j < indices.Length; j++)
                {
                    uint index = 0;
                    uint.TryParse(indices[j], NumberStyles.None, CultureInfo.InvariantCulture, out index);
                    data[i * Width + j] = index;
                }
            }
        }
    }
}