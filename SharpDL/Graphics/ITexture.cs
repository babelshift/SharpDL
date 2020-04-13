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

        void SetBlendMode(BlendMode blendMode);

        void Draw(int x, int y);

        void Draw(float x, float y);
    }
}