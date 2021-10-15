using SharpDL.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpTiles_Example1.Content
{
    /// <summary>
    /// Represents a single object layer from a .tmx file. An object layer contains 0 to many objects.
    /// </summary>
    internal class MapObjectLayer
    {
        private Dictionary<Guid, MapObject> mapObjects = new Dictionary<Guid, MapObject>();

        /// <summary>
        /// The name given to the layer by the author of the map
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A read only collection of objects contained within the layer
        /// </summary>
        public IReadOnlyCollection<MapObject> MapObjects { get { return mapObjects.Values.ToList().AsReadOnly(); } }

        /// <summary>
        /// The default constructor requires a name
        /// </summary>
        /// <param name="name"></param>
        public MapObjectLayer(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Adds a single object to the layer's collection of objects
        /// </summary>
        /// <param name="mapObject"></param>
        public void AddMapObject(MapObject mapObject)
        {
            Utilities.ThrowExceptionIfIsNull(mapObject, "mapObject");
            mapObjects.Add(mapObject.ID, mapObject);
        }

        /// <summary>
        /// Removes the passed map object from the collection of map objects. Does nothing if the map object does not exist in the collection.
        /// </summary>
        /// <param name="mapObject"></param>
        public void RemoveMapObject(MapObject mapObject)
        {
            Utilities.ThrowExceptionIfIsNull(mapObject, "mapObject");
            if (mapObjects.TryGetValue(mapObject.ID, out mapObject))
            {
                mapObjects.Remove(mapObject.ID);
            }
        }

        /// <summary>
        /// Removes a map object from the collection of map objects identified by the passed ID. Does nothing if the map object does not exist in the collection.
        /// </summary>
        /// <param name="mapObjectId"></param>
        public void RemoveMapObject(Guid mapObjectId)
        {
            MapObject mapObject = null;
            if (mapObjects.TryGetValue(mapObjectId, out mapObject))
            {
                mapObjects.Remove(mapObjectId);
            }
        }
    }
}