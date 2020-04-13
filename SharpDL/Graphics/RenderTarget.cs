using SDL2;
using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{
    public class RenderTarget : ITexture
    {
        private IRenderer renderer;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public PixelFormat PixelFormat { get; private set; }
        
        public TextureAccessMode AccessMode { get; private set; }
        
        public IntPtr Handle { get; private set; }

        public RenderTarget(IRenderer renderer, int width, int height)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException("renderer", Errors.E_RENDERER_NULL);
            }

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
            if (renderer == null)
            {
                throw new ArgumentNullException("renderer", Errors.E_RENDERER_NULL);
            }

            int result = SDL.SDL_SetTextureBlendMode(Handle, (SDL.SDL_BlendMode)blendMode);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetTextureBlendMode"));
            }
        }

        public void Draw(int x, int y)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException("renderer", Errors.E_RENDERER_NULL);
            }

            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Errors.E_RENDER_TARGET_NULL);
            }

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
            if (Handle != IntPtr.Zero)
            {
                SDL.SDL_DestroyTexture(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }
}