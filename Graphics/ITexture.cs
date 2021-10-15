using System;

namespace SharpDL.Graphics
{
    public interface ITexture : IDisposable
    {
        /// <summary>Width of the texture's renderable area.
        /// </summary>
        int Width { get; }

        /// <summary>Height of the texture's renderable area.
        /// </summary>
        int Height { get; }

        /// <summary>Pixel format used to represent the memory layout of each pixel such as RGBA8888.
        /// </summary>
        PixelFormat PixelFormat { get; }

        /// <summary>Mode in which a texture can be accessed for updates such as Static and Streaming.
        /// </summary>
        TextureAccessMode AccessMode { get; }

        /// <summary>Unsafe native handle returned by the underlying graphics API. This will probably
        /// be removed from the interface in future versions.
        /// </summary>
        IntPtr Handle { get; }

        /// <summary>Updates the Texture with the contents of a Surface.
        /// </summary>
        /// <param name="surface">Surface used for the new Texture.</param>
        void UpdateSurfaceAndTexture(ISurface surface);

        /// <summary>Sets the blend mode used during rendering of the Texture.
        /// </summary>
        /// <param name="blendMode">Example: alpha blending</param>
        void SetBlendMode(BlendMode blendMode);

        /// <summary>Draws the Texture at position (x,y).
        /// </summary>
        /// <param name="x">X coordinate of the draw position</param>
        /// <param name="y">Y coordinate of the draw position</param>
        void Draw(int x, int y);

        /// <summary>Draws the Texture at position (x,y).
        /// </summary>
        /// <param name="x">X coordinate of the draw position</param>
        /// <param name="y">Y coordinate of the draw position</param>
        void Draw(float x, float y);

        /// <summary>Draws the Texture at position (x,y) and cropped by the bounds of the Rectangle.
        /// </summary>
        /// <param name="x">X coordinate of the draw position</param>
        /// <param name="y">Y coordinate of the draw position</param>
        /// <param name="sourceBounds">Bounds in which the Texture's renderable area is cropped.</param>
        void Draw(int x, int y, Rectangle sourceBounds);

        /// <summary>Draws the Texture at position (x,y) and cropped by the bounds of the Rectangle.
        /// </summary>
        /// <param name="x">X coordinate of the draw position</param>
        /// <param name="y">Y coordinate of the draw position</param>
        /// <param name="sourceBounds">Bounds in which the Texture's renderable area is cropped.</param>
        void Draw(float x, float y, Rectangle sourceBounds);

        /// <summary>Draws the Texture at position (x,y) and rotated around a 2D vector point by a specific angle.
        /// </summary>
        /// <param name="x">X coordinate of the draw position</param>
        /// <param name="y">Y coordinate of the draw position</param>
        /// <param name="angle">Angle of rotation during rendering.</param>
        /// <param name="center">Coordinate around which Texture is rotated.</param>
        void Draw(int x, int y, double angle, Vector center);
    }
}