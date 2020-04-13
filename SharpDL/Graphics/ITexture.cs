using System;

namespace SharpDL.Graphics
{
    public interface ITexture : IDisposable
    {
        int Width { get; }

        int Height { get; }

        PixelFormat PixelFormat { get; }

        TextureAccessMode AccessMode { get; }

        IntPtr Handle { get; }

        void UpdateSurfaceAndTexture(ISurface surface);

        void SetBlendMode(BlendMode blendMode);

        void Draw(int x, int y);

        void Draw(float x, float y);

        void Draw(int x, int y, Rectangle sourceBounds);

        void Draw(float x, float y, Rectangle sourceBounds);

        void Draw(int x, int y, double angle, Vector center);
    }
}