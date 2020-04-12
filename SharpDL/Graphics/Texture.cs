﻿using SDL2;
using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{

    public class Texture : ITexture
    {
        private IRenderer renderer;
        private SafeTextureHandle safeHandle;

        public string FilePath { get; private set; }

        public uint PixelFormat { get; private set; }

        public int Access { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Surface Surface { get; private set; }

        public IntPtr Handle { get { return safeHandle.DangerousGetHandle(); } }

        public TextureAccessMode AccessMode { get; private set; }

        public Texture(IRenderer renderer, Surface surface)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException("renderer", Errors.E_RENDERER_NULL);
            }
            if (surface == null)
            {
                throw new ArgumentNullException("surface", Errors.E_SURFACE_NULL);
            }

            this.renderer = renderer;
            FilePath = surface.FilePath;
            AccessMode = TextureAccessMode.Static;
            Surface = surface;

            CreateTextureAndCleanup(surface.Width, surface.Height);
        }

        public void UpdateSurfaceAndTexture(Surface surface)
        {
            if (surface == null)
            {
                throw new ArgumentNullException("surface", Errors.E_SURFACE_NULL);
            }

            // Don't want to dispose this entire Texture object because that would kill the instance.
            // Instead we just dispose our Texture handle and create a new one.
            safeHandle.Dispose();
            Surface = surface;

            CreateTextureAndCleanup(surface.Width, surface.Height);
        }

        private void CreateTextureAndCleanup(int width, int height)
        {
            bool success = CreateTexture(width, height);

            if (!success)
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_CreateTextureFromSurface"));
            }

            CleanupAndQueryTexture();
        }

        private bool CreateTexture(int width, int height)
        {
            bool success = false;

            if (Surface == null) { return success; }

            IntPtr unsafeHandle = SDL.SDL_CreateTextureFromSurface(renderer.Handle, Surface.Handle);

            if (unsafeHandle != IntPtr.Zero)
            {
                success = true;
            }

            safeHandle = new SafeTextureHandle(unsafeHandle);

            return success;
        }

        private void CleanupAndQueryTexture()
        {
            Surface.Dispose();

            uint format;
            int access, width, height;
            SDL.SDL_QueryTexture(Handle, out format, out access, out width, out height);

            PixelFormat = format;
            Access = access;
            Width = width;
            Height = height;
        }

        public void SetBlendMode(BlendMode blendMode)
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Errors.E_TEXTURE_NULL);
            }

            int result = SDL2.SDL.SDL_SetTextureBlendMode(Handle, (SDL.SDL_BlendMode)blendMode);

            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetTextureBlendMode"));
            }
        }

        public void Draw(int x, int y, double angle, Vector center)
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Errors.E_TEXTURE_NULL);
            }
            if (renderer == null)
            {
                throw new InvalidOperationException(Errors.E_RENDERER_NULL);
            }

            renderer.RenderTexture(Handle, x, y, Width, Height, angle, center);
        }

        public void Draw(int x, int y, Rectangle sourceBounds)
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Errors.E_TEXTURE_NULL);
            }
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
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Errors.E_TEXTURE_NULL);
            }
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

        public void SetColorMod(byte r, byte g, byte b)
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Errors.E_TEXTURE_NULL);
            }

            int result = SDL.SDL_SetTextureColorMod(Handle, r, g, b);

            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetTextureColorMod: {0}"));
            }
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
                Surface.Dispose();
                safeHandle.Dispose();
            }
        }
    }
}