using SharpDL.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace SharpDL.Tiles
{
    public class TileSetContent
    {
        private Dictionary<int, PropertyCollection> tileProperties = new Dictionary<int, PropertyCollection>();

        public int FirstGID { get; private set; }

        public string Name { get; private set; }

        public int TileWidth { get; private set; }

        public int TileHeight { get; private set; }

        public string ImageSource { get; private set; }

        public int Spacing { get; private set; }

        public int Margin { get; private set; }

        public Color? ColorKey { get; private set; }

        public ITexture Texture { get; set; }

        public IList<TileContent> Tiles { get; private set; }

        public IReadOnlyDictionary<int, PropertyCollection> TileProperties { get { return tileProperties; } }

        public TileSetContent(XmlNode tileSetNode)
        {
            Utilities.ThrowExceptionIfIsNull(tileSetNode, "tileSetNode");

            Tiles = new List<TileContent>();

            FirstGID = Utilities.TryToParseInt(tileSetNode.Attributes[AttributeNames.TileSetAttributes.FirstGID].Value);

            XmlNode preparedNode = PrepareXmlNode(tileSetNode);
            Initialize(tileSetNode);
        }

        private void Initialize(XmlNode tileSetNode)
        {
            Utilities.ThrowExceptionIfIsNull(tileSetNode, "tileSetNode");
            Utilities.ThrowExceptionIfAttributeIsNull(tileSetNode, "TileSetNode", AttributeNames.TileSetAttributes.Name);
            Utilities.ThrowExceptionIfAttributeIsNull(tileSetNode, "TileSetNode", AttributeNames.TileSetAttributes.TileWidth);
            Utilities.ThrowExceptionIfAttributeIsNull(tileSetNode, "TileSetNode", AttributeNames.TileSetAttributes.TileHeight);
            Utilities.ThrowExceptionIfChildNodeIsNull(tileSetNode, "ImageNode", AttributeNames.TileSetAttributes.Image);

            Name = tileSetNode.Attributes[AttributeNames.TileSetAttributes.Name].Value;

            TileWidth = Utilities.TryToParseInt(tileSetNode.Attributes[AttributeNames.TileSetAttributes.TileWidth].Value);
            
            TileHeight = Utilities.TryToParseInt(tileSetNode.Attributes[AttributeNames.TileSetAttributes.TileHeight].Value);

            // spacing is optional
            if (tileSetNode.Attributes[AttributeNames.TileSetAttributes.Spacing] != null)
            {
                Spacing = Utilities.TryToParseInt(tileSetNode.Attributes[AttributeNames.TileSetAttributes.Spacing].Value);
            }

            // margin is optional
            if (tileSetNode.Attributes[AttributeNames.TileSetAttributes.Margin] != null)
            {
                Margin = Utilities.TryToParseInt(tileSetNode.Attributes[AttributeNames.TileSetAttributes.Margin].Value);
            }

            XmlNode imageNode = tileSetNode[AttributeNames.TileSetAttributes.Image];

            Utilities.ThrowExceptionIfAttributeIsNull(imageNode, "ImageNode", AttributeNames.TileSetAttributes.Source);

            ImageSource = imageNode.Attributes[AttributeNames.TileSetAttributes.Source].Value;

            // transparency is optional
            if (imageNode.Attributes[AttributeNames.TileSetAttributes.Transparency] != null)
            {
                string colorCode = imageNode.Attributes[AttributeNames.TileSetAttributes.Transparency].Value;
                ColorKey = new Color(colorCode);
            }

            foreach (XmlNode tile in tileSetNode.SelectNodes(AttributeNames.TileSetAttributes.Tile))
            {
                int id = Utilities.TryToParseInt(tile.Attributes[AttributeNames.TileSetAttributes.ID].Value);
                int tileId = FirstGID + id;

                PropertyCollection properties = new PropertyCollection();

                if (tile[AttributeNames.TileSetAttributes.Properties] != null)
                {
                    properties = new PropertyCollection(tile[AttributeNames.TileSetAttributes.Properties]);
                }

                tileProperties.Add(id, properties);
            }
        }

        protected virtual XmlNode PrepareXmlNode(XmlNode root)
        {
            Utilities.ThrowExceptionIfIsNull(root, "root");

            return root;
        }
    }
}