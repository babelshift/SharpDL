using SharpDL;
using SharpDL.Graphics;
using SharpDL.Tiles;
using System;
using System.Collections.Generic;

namespace SharpTiles_Example1.Content
{
    /// <summary>
    /// Represents a single .tmx file from Tiled Map Editor. Tiled maps contain tile layers (which contain tiles) and object layers (which contain objects).
    /// </summary>
    internal class TiledMap : IDisposable
    {
        private List<TileLayer> tileLayers = new List<TileLayer>();
        private List<MapObjectLayer> mapObjectLayers = new List<MapObjectLayer>();

        /// <summary>
        /// The number of tiles that make up this map in the horizontal direction (defined in TMX file)
        /// </summary>
        public int HorizontalTileCount { get; private set; }

        /// <summary>
        /// The number of tiles that make up this map in the vertical direction (defined in TMX file)
        /// </summary>
        public int VerticalTileCount { get; private set; }

        /// <summary>
        /// The number of tiles across (left to right) that make up this map
        /// </summary>
        public int PixelWidth { get; private set; }

        /// <summary>
        /// The number of tiles down (top to bottom) that make up this map
        /// </summary>
        public int PixelHeight { get; private set; }

        /// <summary>
        /// The width of each tile in the map (all tiles are the same width)
        /// </summary>
        public int TileWidth { get; private set; }

        /// <summary>
        /// The height of each tile in the map (all tiles are the same height)
        /// </summary>
        public int TileHeight { get; private set; }

        /// <summary>
        /// Tile layers contain Tiles read from the Tiled map editor.
        /// </summary>
        public IReadOnlyCollection<TileLayer> TileLayers { get { return tileLayers.AsReadOnly(); } }

        /// <summary>
        /// Map object layers contain map objects read from the Tiled map editor.
        /// </summary>
        public IReadOnlyCollection<MapObjectLayer> MapObjectLayers { get { return mapObjectLayers.AsReadOnly(); } }

        /// <summary
        /// >Default constructor creates a map from a .tmx file and creates any associated tileset textures by using the passed renderer.
        /// </summary>
        /// <param name="filePath">Path to the .tmx file to load</param>
        /// <param name="renderer">Renderer object used to load tileset textures</param>
        public TiledMap(string filePath, IRenderer renderer, string contentRoot = "")
        {
            MapContent mapContent = new MapContent(filePath, renderer, contentRoot);

            TileWidth = mapContent.TileWidth;
            TileHeight = mapContent.TileHeight;

            PixelWidth = mapContent.Width * TileWidth;
            PixelHeight = mapContent.Height * TileHeight;

            HorizontalTileCount = mapContent.Width;
            VerticalTileCount = mapContent.Height;

            CreateLayers(mapContent);
        }

        /// <summary>
        /// Create tile layers and object layers based on what we find in the Tiled Map TMX file.
        /// </summary>
        /// <param name="mapContent">Map contentManager.</param>
        private void CreateLayers(MapContent mapContent)
        {
            if (mapContent == null) throw new ArgumentNullException("mapContent");

            foreach (LayerContent layerContent in mapContent.Layers)
            {
                if (layerContent is TileLayerContent)
                {
                    TileLayer tileLayer = CreateTileLayer(layerContent, mapContent.TileSets);
                    tileLayers.Add(tileLayer);
                }
                else if (layerContent is ObjectLayerContent)
                {
                    MapObjectLayer mapObjectLayer = CreateObjectLayer(layerContent, mapContent.Orientation);
                    mapObjectLayers.Add(mapObjectLayer);
                }
            }
        }

        /// <summary>
        /// Creates a tile layer by reading the contentManager we got from the SharpTiles library. This will create our tile layers of type "Floor" and "Objects".
        /// </summary>
        /// <param name="layerContent"></param>
        /// <param name="tileSets"></param>
        /// <returns></returns>
        private TileLayer CreateTileLayer(LayerContent layerContent, IEnumerable<TileSetContent> tileSets)
        {
            if (layerContent == null) throw new ArgumentNullException("layerContent");
            if (tileSets == null) throw new ArgumentNullException("tileSets");

            TileLayerContent tileLayerContent = layerContent as TileLayerContent;

            TileLayer tileLayer = new TileLayer(layerContent.Name, tileLayerContent.Width, tileLayerContent.Height);
            foreach (uint tileID in tileLayerContent.Data)
            {
                uint flippedHorizontallyFlag = 0x80000000;
                uint flippedVerticallyFlag = 0x40000000;
                int tileIndex = (int)(tileID & ~(flippedVerticallyFlag | flippedHorizontallyFlag));
                Tile tile = CreateTile(tileIndex, tileSets);
                tileLayer.AddTile(tile);
            }

            return tileLayer;
        }

        /// <summary>
        /// Based on a passed tile index, create a Tile by looking up which TileSet it belongs to, assign the proper TilSet texture,
        /// and find the bounds of the rectangle that encompasses the correct tile texture within the total tileset texture.
        /// </summary>
        /// <param name="tileIndex">Index of the tile (GID) within the map file</param>
        /// <param name="tileSets">Enumerable list of tilesets used to find out which tileset a tile belongs to</param>
        /// <param name="tileLayerType"></param>
        /// <returns></returns>
        private Tile CreateTile(int tileIndex, IEnumerable<TileSetContent> tileSets)
        {
            if (tileSets == null) throw new ArgumentNullException("tileSets");

            Tile tile = new Tile();

            // we don't want to look up tiles with ID 0 in tile sets because Tiled Map Editor treats ID 0 as an empty tile
            if (tileIndex > Tile.EmptyTileID)
            {
                ITexture tileSetTexture = null;
                Rectangle source = new Rectangle();

                foreach (TileSetContent tileSet in tileSets)
                {
                    if (tileIndex - tileSet.FirstGID < tileSet.Tiles.Count)
                    {
                        tileSetTexture = tileSet.Texture;
                        source = tileSet.Tiles[tileIndex - tileSet.FirstGID].SourceTextureBounds;
                        break;
                    }
                }

                tile = new Tile(tileSetTexture, source, TileWidth, TileHeight);
            }

            return tile;
        }

        /// <summary>
        /// Creates the proper map object layer based on the layer name such as collidables and path nodes.
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
        private MapObjectLayer CreateObjectLayer(LayerContent layer, Orientation orientation)
        {
            if (layer == null) throw new ArgumentNullException("layer");

            ObjectLayerContent objectLayerContent = layer as ObjectLayerContent;

            MapObjectLayer mapObjectLayer = new MapObjectLayer(objectLayerContent.Name);

            foreach (ObjectContent objectContent in objectLayerContent.MapObjects)
            {
                MapObject mapObject = new MapObject(objectContent.Name, objectContent.Bounds, orientation, objectContent.Properties);
                mapObjectLayer.AddMapObject(mapObject);
            }

            return mapObjectLayer;
        }

        /// <summary>
        /// Draws the tile to the passed renderer if the tile is not empty. The draw will occur at the center of the tile's texture.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="renderer"></param>
        public void Draw(GameTime gameTime, IRenderer renderer)
        {
            foreach (var tileLayer in TileLayers)
            {
                foreach (var tile in tileLayer.Tiles)
                {
                    tile.Draw(gameTime);
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Always dispose of the tile map because its layers contain native textured tiles.
        /// </summary>
        /// <param name="isDisposing"></param>
        private void Dispose(bool isDisposing)
        {
            foreach (TileLayer tileLayer in tileLayers)
                tileLayer.Dispose();
        }
    }
}