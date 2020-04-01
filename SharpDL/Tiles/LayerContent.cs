using System.Xml;

namespace SharpDL.Tiles
{
    public abstract class LayerContent
    {
        private PropertyCollection properties = new PropertyCollection();

        public string Name { get; set; }

        public string Type { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public float Opacity { get; private set; }

        public bool IsVisible { get; private set; }

        public PropertyCollection Properties { get { return properties; } }

        public LayerContent(XmlNode layerNode)
        {
            Utilities.ThrowExceptionIfIsNull(layerNode, "layerNode");
            Utilities.ThrowExceptionIfAttributeIsNull(layerNode, "layerNode", AttributeNames.LayerAttributes.Name);
            Utilities.ThrowExceptionIfAttributeIsNull(layerNode, "layerNode", AttributeNames.LayerAttributes.Width);
            Utilities.ThrowExceptionIfAttributeIsNull(layerNode, "layerNode", AttributeNames.LayerAttributes.Height);

            Type = layerNode.Name;
            Name = layerNode.Attributes[AttributeNames.LayerAttributes.Name].Value;

            Width = Utilities.TryToParseInt(layerNode.Attributes[AttributeNames.LayerAttributes.Width].Value);
            Height = Utilities.TryToParseInt(layerNode.Attributes[AttributeNames.LayerAttributes.Height].Value);

            // opacity is optional
            if (layerNode.Attributes[AttributeNames.LayerAttributes.Opacity] != null)
            {
                Opacity = Utilities.TryToParseFloat(layerNode.Attributes[AttributeNames.LayerAttributes.Opacity].Value);
            }

            // visibility is optional
            if (layerNode.Attributes[AttributeNames.LayerAttributes.Visible] != null)
            {
                IsVisible = Utilities.TryToParseBool(layerNode.Attributes[AttributeNames.LayerAttributes.Visible].Value);
            }

            // properties are optional
            if (layerNode.Attributes[AttributeNames.LayerAttributes.Properties] != null)
            {
                properties = new PropertyCollection(layerNode.Attributes[AttributeNames.LayerAttributes.Properties]);
            }
        }
    }
}