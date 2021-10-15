using System;
using System.Xml;

namespace SharpDL.Tiles
{
    public class ExternalTileSetContent : TileSetContent
    {
        public ExternalTileSetContent(XmlNode node)
            : base(node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("root");
            }
        }

        protected override XmlNode PrepareXmlNode(XmlNode root)
        {
            Utilities.ThrowExceptionIfIsNull(root, "root");
            Utilities.ThrowExceptionIfAttributeIsNull(root, "root", AttributeNames.TileSetAttributes.Source);

            XmlDocument externalTileSet = new XmlDocument();
            externalTileSet.Load(root.Attributes[AttributeNames.TileSetAttributes.Source].Value);
            return externalTileSet[AttributeNames.TileSetAttributes.TileSet];
        }
    }
}