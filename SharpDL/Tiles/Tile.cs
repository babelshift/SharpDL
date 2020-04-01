using SharpDL;
using SharpDL.Graphics;
using System;

namespace SharpDL.Tiles
{
    /// <summary>
    /// Represents a single tile from a .tmx file in Tiled Map Editor. A tile can contain a texture and properties.
    /// </summary>
    internal class Tile : IDisposable
    {
        // Tiled Editor assigns the id of '0' to tiles with no textures
        public const int EmptyTileID = 0;

        private int index;

        /// <summary>
        /// Returns the index in the tile layer collection
        /// </summary>
        public int Index { get { return index; } }

        /// <summary>
        /// Width of the tile in pixels
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Height of the tile in pixels
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// The position on the [x,y] coordinate tile map where this tile is located.
        /// </summary>
        public Point GridPosition { get; private set; }

        /// <summary>
        /// Texture from which to select a rectangle source texture (similar to selecting from a sprite sheet)
        /// </summary>
        public Texture Texture { get; private set; }

        /// <summary>
        /// Rectangle determining where in the Texture to select the specific texture for our sprite or frame
        /// </summary>
        public Rectangle SourceTextureBounds { get; private set; }

        /// <summary>
        /// Tiles are empty if they have no texture assigned within Tiled Map Editor
        /// </summary>
        public bool IsEmpty { get; private set; }

        /// <summary>
        /// Default empty constructor creates an empty tile
        /// </summary>
        public Tile()
        {
            index = -1;
            IsEmpty = true;
        }

        /// <summary>
        /// Main constructor used to instantiate a tile when data is known at import
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="source"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Tile(Texture texture, Rectangle source, int width, int height)
        {
            index = -1;
            IsEmpty = false;
            Texture = texture;
            SourceTextureBounds = source;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Sets the grid position index of the tile within the map. This is based on the number of tiles already in the map and the width of its parent tile layer.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="tileLayerWidth"></param>
        public void SetIndex(int index, int tileLayerWidth)
        {
            this.index = index;
            if (index > -1)
            {
                GridPosition = new Point(index % tileLayerWidth, index / tileLayerWidth); // TODO: need to update this based on orientation?
            }
        }

        /// <summary>
        /// Draws the tile to the passed renderer if the tile is not empty. The draw will occur at the center of the tile's texture. By default,
        /// this method will only render in an orthogonal projection. If isometric is required, inherit and override this method with proper behaviors.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="renderer"></param>
        public virtual void Draw(GameTime gameTime, Renderer renderer)
        {
            if (IsEmpty) return;

            Texture.Draw(
                GridPosition.X * Width,
                GridPosition.Y * Height,
                SourceTextureBounds);
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
        /// Always dispose of the texture because it's instantiated in native code.
        /// </summary>
        /// <param name="isDisposing"></param>
        private void Dispose(bool isDisposing)
        {
            if (Texture != null)
                Texture.Dispose();
        }
    }
}