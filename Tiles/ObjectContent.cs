using SharpDL.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace SharpDL.Tiles
{
    public enum MapObjectType : byte
    {
        Plain,
        Tile,
        Polygon,
        Polyline
    }

    public class ObjectContent
    {
        private PropertyCollection properties = new PropertyCollection();
        private List<Point> points = new List<Point>();
        private MapObjectType objectType = MapObjectType.Plain;

        public MapObjectType ObjectType { get { return objectType; } }

        public string Name { get; private set; }

        public string Type { get; private set; }

        public Rectangle Bounds { get; private set; }

        public int GID { get; private set; }

        public IReadOnlyCollection<Point> Points { get { return points.AsReadOnly(); } }

        public PropertyCollection Properties { get { return properties; } }

        public ObjectContent(XmlNode node)
        {
            Utilities.ThrowExceptionIfIsNull(node, "node");
            Utilities.ThrowExceptionIfAttributeIsNull(node, "node", AttributeNames.MapObjectAttributes.Name);
            Utilities.ThrowExceptionIfAttributeIsNull(node, "node", AttributeNames.MapObjectAttributes.X);
            Utilities.ThrowExceptionIfAttributeIsNull(node, "node", AttributeNames.MapObjectAttributes.Y);

            Name = node.Attributes[AttributeNames.MapObjectAttributes.Name].Value;

            if (node.Attributes[AttributeNames.MapObjectAttributes.Type] != null)
            {
                Type = node.Attributes[AttributeNames.MapObjectAttributes.Type].Value;
            }

            if (node[AttributeNames.MapObjectAttributes.Properties] != null)
            {
                properties = new PropertyCollection(node[AttributeNames.MapObjectAttributes.Properties]);
            }

            int width = 0;
            int height = 0;

            int x = Utilities.TryToParseInt(node.Attributes[AttributeNames.MapObjectAttributes.X].Value);

            int y = Utilities.TryToParseInt(node.Attributes[AttributeNames.MapObjectAttributes.Y].Value);

            if (node.Attributes[AttributeNames.MapObjectAttributes.Width] != null)
            {
                width = Utilities.TryToParseInt(node.Attributes[AttributeNames.MapObjectAttributes.Width].Value);
            }

            if (node.Attributes[AttributeNames.MapObjectAttributes.Height] != null)
            {
                height = Utilities.TryToParseInt(node.Attributes[AttributeNames.MapObjectAttributes.Height].Value);
            }

            Bounds = new Rectangle(x, y, width, height);

            string objectPoints = String.Empty;

            if (node.Attributes[AttributeNames.MapObjectAttributes.GID] != null)
            {
                objectType = MapObjectType.Tile;
                GID = Utilities.TryToParseInt(node.Attributes[AttributeNames.MapObjectAttributes.GID].Value);
            }
            else if (node.Attributes[AttributeNames.MapObjectAttributes.Polygon] != null)
            {
                objectType = MapObjectType.Polygon;
                objectPoints = node.Attributes[AttributeNames.MapObjectAttributes.Polygon].Value;
            }
            else if (node.Attributes[AttributeNames.MapObjectAttributes.Polyline] != null)
            {
                objectType = MapObjectType.Polyline;
                objectPoints = node.Attributes[AttributeNames.MapObjectAttributes.Polyline].Value;
            }

            if (!String.IsNullOrEmpty(objectPoints))
            {
                string[] splitPoints = objectPoints.Split(' ');
                foreach (string splitPoint in splitPoints)
                {
                    string[] coordinates = splitPoint.Split(',');

                    float coordinateX = 0f, coordinateY = 0f;
                    float.TryParse(coordinates[0], NumberStyles.None, CultureInfo.InvariantCulture, out coordinateX);
                    float.TryParse(coordinates[1], NumberStyles.None, CultureInfo.InvariantCulture, out coordinateY);

                    points.Add(new Point((int)x, (int)y));
                }
            }
        }
    }
}