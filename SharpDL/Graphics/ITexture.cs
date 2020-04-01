using System;

namespace SharpDL.Graphics
{
    public interface ITexture : IDisposable
    {
        uint PixelFormat { get; }

        int Width { get; }

        int Height { get; }

        IntPtr Handle { get; }

        TextureAccessMode AccessMode { get; }

        void SetBlendMode(BlendMode blendMode);

        void Draw(int x, int y);

        void Draw(float x, float y);
    }
}