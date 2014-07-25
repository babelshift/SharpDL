using SDL2;
using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{
    public class RenderTarget : ITexture
    {
        private Renderer renderer;

        public uint PixelFormat { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public IntPtr Handle { get; private set; }

        public TextureAccessMode AccessMode { get; private set; }

        public RenderTarget(Renderer renderer, int width, int height)
        {
            Assert.IsNotNull(renderer, Errors.E_RENDERER_NULL);

            this.renderer = renderer;
            Width = width;
            Height = height;
            AccessMode = TextureAccessMode.Target;

            Handle = SDL.SDL_CreateTexture(renderer.Handle, SDL.SDL_PIXELFORMAT_RGBA8888, (int)AccessMode, Width, Height);
            if (Handle == IntPtr.Zero)
                throw new InvalidOperationException(Utilities.GetErrorMessage("RenderTarget"));
        }

        public void SetBlendMode(BlendMode blendMode)
        {
            Assert.IsNotNull(renderer, Errors.E_RENDER_TARGET_NULL);

            int result = SDL.SDL_SetTextureBlendMode(Handle, (SDL.SDL_BlendMode)blendMode);
            if (Utilities.IsError(result))
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetTextureBlendMode"));
        }

        public void Draw(int x, int y)
        {
            Assert.IsNotNull(renderer, Errors.E_RENDER_TARGET_NULL);
            Assert.IsNotNull(renderer, Errors.E_RENDERER_NULL);

            renderer.RenderTexture(Handle, x, y, Width, Height);
        }

        public void Draw(float x, float y)
        {
            Draw((int)x, (int)y);
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            SDL.SDL_DestroyTexture(Handle);
            Handle = IntPtr.Zero;
        }
    }
}