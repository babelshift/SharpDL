using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SharpDL.Tiles
{
    /// <summary>
    /// Represents a single tile layer from a .tmx file. A tile layer contains 0 to many tiles.
    /// </summary>
    internal class TileLayer : IDisposable
    {
        private Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();

        /// <summary>
        /// The name given to the layer by the author of the map
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The width of the layer (usually the sum of all widths of tiles)
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The height of the layer (usually the sum of all heights of tiles)
        /// </summary>
        public int Height { get; private set; }

        public IReadOnlyCollection<Tile> Tiles { get { return tiles.Values.ToList().AsReadOnly(); } }

        /// <summary>
        /// A read only indexable list of the tiles contained within the layer
        /// </summary>
        public IReadOnlyDictionary<int, Tile> InternalTiles { get { return new ReadOnlyDictionary<int, Tile>(tiles); } }

        /// <summary>
        /// Default constructor requires a name, the width, and the height of the layer from the .tmx file.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public TileLayer(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Adds a single tile to the layer's collection of tiles.
        /// </summary>
        /// <param name="tile"></param>
        public void AddTile(Tile tile)
        {
            tile.SetIndex(tiles.Count, Width);

            tiles.Add(tile.Index, tile);
        }

        /// <summary>
        /// Removes the passed tile from the collection of tiles. Does nothing if the tile does not exist in the collection.
        /// </summary>
        /// <param name="mapObject"></param>
        public void RemoveTile(Tile tile)
        {
            if (tiles.TryGetValue(tile.Index, out tile))
            {
                tiles.Remove(tile.Index);
            }
        }

        /// <summary>
        /// Removes a tile from the collection of tiles identified by the passed index. Does nothing if the tile does not exist in the collection.
        /// </summary>
        /// <param name="mapObject"></param>
        public void RemoveTile(int index)
        {
            Tile tile = null;
            if (tiles.TryGetValue(index, out tile))
            {
                tiles.Remove(index);
            }
        }

        /// <summary>
        /// If you override this method, you *must* call the base Dispose method in order to remove any resources allocated in the base class.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Always dispose of the tile layers because they contain native textured tiles.
        /// </summary>
        /// <param name="isDisposing"></param>
        private void Dispose(bool isDisposing)
        {
            foreach (Tile tile in tiles.Values)
            {
                tile.Dispose();
            }
        }
    }
}