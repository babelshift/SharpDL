using SharpDL.Graphics;
using System;

namespace SharpDL.Tiles
{
    /// <summary>
    /// Represents a single object from a .tmx file. An object is a bounded entity which can
    /// define extra behaviors to regions of the map such as spawn points, collision boundaries, and more.
    /// </summary>
    internal class MapObject
    {
        public Guid ID { get; private set; }

        /// <summary>
        /// The name given to the object by the author of the map
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The rectangular bounds of the object as defined by the author of the map
        /// </summary>
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// The orientation of projection as defined by the author of the map
        /// </summary>
        public Orientation Orientation { get; private set; }

        /// <summary>
        /// A collection of properties assigned to the object as defined by the author of the map
        /// </summary>
        public PropertyCollection Properties { get; private set; }

        /// <summary>
        /// Default constructor requires a name, the bounds of the object, the orientation of projection, and a collection of properties assigned to the object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bounds"></param>
        /// <param name="orientation"></param>
        /// <param name="properties"></param>
        public MapObject(string name, Rectangle bounds, Orientation orientation, PropertyCollection properties)
        {
            Name = name;
            Bounds = bounds;
            Orientation = orientation;
            Properties = properties;
            ID = Guid.NewGuid();
        }
    }
}