﻿using SDL2;
using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{
    public class Texture : ITexture
    {
        private IRenderer renderer;
        private SafeTextureHandle safeHandle;

        public int Width { get; private set; }

        public int Height { get; private set; }

        public PixelFormat PixelFormat { get; private set; }

        public TextureAccessMode AccessMode { get; private set; }

        public IntPtr Handle { get { return safeHandle.DangerousGetHandle(); } }

        internal Texture(IRenderer renderer, int width, int height, PixelFormat pixelFormat, TextureAccessMode accessMode)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            this.renderer = renderer;

            IntPtr unsafeHandle = CreateTexture(width, height, pixelFormat, accessMode);
            safeHandle = new SafeTextureHandle(unsafeHandle);

            QueryTexture(unsafeHandle);
        }

        internal Texture(IRenderer renderer, ISurface surface)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            if (surface == null)
            {
                throw new ArgumentNullException(nameof(surface));
            }

            this.renderer = renderer;

            CreateTextureAndCleanup(surface);
        }

        internal IntPtr CreateTexture(int width, int height, PixelFormat pixelFormat, TextureAccessMode accessMode)
        {
            uint mappedPixelFormat = PixelFormatMap.EnumToSDL(pixelFormat);

            IntPtr unsafeHandle = SDL.SDL_CreateTexture(renderer.Handle, mappedPixelFormat, (int)accessMode, width, height);
            if (unsafeHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_CreateTextureFromSurface"));
            }

            return unsafeHandle;
        }

        internal IntPtr CreateTextureFromSurface(ISurface surface)
        {
            IntPtr unsafeHandle = SDL.SDL_CreateTextureFromSurface(renderer.Handle, surface.Handle);

            if (unsafeHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_CreateTextureFromSurface"));
            }

            return unsafeHandle;
        }
        
        /// <summary>Look up the texture details to make sure it matches what we expect
        /// </summary>
        /// <param name="textureHandle">Unsafe handle to the texture</param>
        private void QueryTexture(IntPtr textureHandle)
        {
            SDL.SDL_QueryTexture(textureHandle, out uint format, out int access, out int width, out int height);
            PixelFormat = PixelFormatMap.SDLToEnum(format);
            AccessMode = (TextureAccessMode)access;
            Width = width;
            Height = height;
        }

        public void UpdateSurfaceAndTexture(ISurface surface)
        {
            if (surface == null)
            {
                throw new ArgumentNullException(nameof(surface));
            }

            // Don't want to dispose this entire Texture object because that would kill the instance.
            // Instead we just dispose our Texture handle and create a new one.
            safeHandle.Dispose();

            CreateTextureAndCleanup(surface);
        }
        
        private void CreateTextureAndCleanup(ISurface surface)
        {
            IntPtr unsafeHandle = CreateTextureFromSurface(surface);
            safeHandle = new SafeTextureHandle(unsafeHandle);

            // We are done with the in memory surface
            surface.Dispose();

            QueryTexture(unsafeHandle);
        }

        public void SetBlendMode(BlendMode blendMode)
        {
            int result = SDL2.SDL.SDL_SetTextureBlendMode(Handle, (SDL.SDL_BlendMode)blendMode);

            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetTextureBlendMode"));
            }
        }
        
        public void SetColorMod(byte r, byte g, byte b)
        {
            int result = SDL.SDL_SetTextureColorMod(Handle, r, g, b);

            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetTextureColorMod: {0}"));
            }
        }

        public void Draw(int x, int y, double angle, Vector center)
        {
            if (renderer == null)
            {
                throw new InvalidOperationException(Errors.E_RENDERER_NULL);
            }
            
            renderer.RenderTexture(Handle, x, y, Width, Height, angle, center);
        }

        public void Draw(int x, int y, Rectangle sourceBounds)
        {
            if (renderer == null)
            {
                throw new InvalidOperationException(Errors.E_RENDERER_NULL);
            }

            renderer.RenderTexture(Handle, x, y, sourceBounds);
        }

        public void Draw(float x, float y, Rectangle sourceBounds)
        {
            Draw((int)x, (int)y, sourceBounds);
        }

        public void Draw(int x, int y)
        {
            if (renderer == null)
            {
                throw new InvalidOperationException(Errors.E_RENDERER_NULL);
            }

            renderer.RenderTexture(Handle, x, y, Width, Height);
        }

        public void Draw(float x, float y)
        {
            Draw((int)x, (int)y);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                safeHandle.Dispose();
            }
        }
    }
}