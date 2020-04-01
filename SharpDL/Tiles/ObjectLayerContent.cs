using SharpDL.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace SharpDL.Tiles
{
    public class ObjectLayerContent : LayerContent
    {
        private List<ObjectContent> mapObjects = new List<ObjectContent>();

        public Color Color { get; private set; }

        public IReadOnlyCollection<ObjectContent> MapObjects { get { return mapObjects.AsReadOnly(); } }

        public ObjectLayerContent(XmlNode node)
            : base(node)
        {
            Utilities.ThrowExceptionIfIsNull(node, "node");

            if (node.Attributes[AttributeNames.MapObjectLayerAttributes.Color] != null)
            {
                string color = node.Attributes[AttributeNames.MapObjectLayerAttributes.Color].Value.Substring(1);

                string redValue = color.Substring(0, 2);
                string greenValue = color.Substring(2, 2);
                string blueValue = color.Substring(4, 2);

                byte red = (byte)int.Parse(redValue, NumberStyles.AllowHexSpecifier);
                byte green = (byte)int.Parse(greenValue, NumberStyles.AllowHexSpecifier);
                byte blue = (byte)int.Parse(blueValue, NumberStyles.AllowHexSpecifier);

                Color = new Color(red, green, blue);
            }

            foreach (XmlNode objectNode in node.SelectNodes(AttributeNames.MapObjectLayerAttributes.Object))
            {
                string originalObjectName = String.Empty;
                if (objectNode.Attributes[AttributeNames.MapObjectAttributes.Name] != null)
                {
                    originalObjectName = objectNode.Attributes[AttributeNames.MapObjectAttributes.Name].Value;

                    string finalObjectName = originalObjectName;
                    int duplicateCount = 2;

                    while (mapObjects.Any(mo => mo.Name == finalObjectName))
                    {
                        finalObjectName = String.Format("{0}{1}", originalObjectName, duplicateCount);
                        duplicateCount++;
                    }

                    objectNode.Attributes[AttributeNames.MapObjectAttributes.Name].Value = finalObjectName;
                }

                ObjectContent mapObject = new ObjectContent(objectNode);
                mapObjects.Add(mapObject);
            }
        }
    }
}