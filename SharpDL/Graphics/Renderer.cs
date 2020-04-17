using Microsoft.Extensions.Logging;
using SDL2;
using SharpDL.Shared;
using System;
using System.Collections.Generic;

namespace SharpDL.Graphics
{
    public class Renderer : IRenderer
    {
        private readonly ILogger<Renderer> logger;

        private SafeRendererHandle safeHandle;

        private List<RendererFlags> flags = new List<RendererFlags>();

        public IWindow Window { get; private set; }

        public int Index { get; private set; }

        public IEnumerable<RendererFlags> Flags { get { return flags; } }

        public IntPtr Handle { get { return safeHandle.DangerousGetHandle(); } }

        internal Renderer(IWindow window, ILogger<Renderer> logger = null)
            : this(window, 0, RendererFlags.None)
        {
        }

        internal Renderer(IWindow window, int index, RendererFlags flags, ILogger<Renderer> logger = null)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window), "Window has not been initialized. You must first create a Window before creating a Renderer.");
            }

            if (index < -1)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            this.logger = logger;
            Window = window;
            Index = index;

            List<RendererFlags> copyFlags = new List<RendererFlags>();
            foreach (RendererFlags flag in Enum.GetValues(typeof(RendererFlags)))
            {
                if (flags.HasFlag(flag))
                {
                    this.flags.Add(flag);
                }
            }

            IntPtr unsafeHandle = SDL.SDL_CreateRenderer(Window.Handle, Index, (SDL.SDL_RendererFlags)flags);
            if (unsafeHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_CreateRenderer"));
            }
            safeHandle = new SafeRendererHandle(unsafeHandle);
        }

        public void ClearScreen()
        {
            int result = SDL.SDL_RenderClear(Handle);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderClear"));
            }
        }

        public void RenderTexture(ITexture texture, float positionX, float positionY, int sourceWidth, int sourceHeight, double angle, Vector center)
        {
            if (texture.Handle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(texture.Handle));
            }

            // SDL only accepts integer positions (x,y) in the rendering Rect
            SDL.SDL_Rect destinationRectangle = new SDL.SDL_Rect() { x = (int)positionX, y = (int)positionY, w = sourceWidth, h = sourceHeight };
            SDL.SDL_Rect sourceRectangle = new SDL.SDL_Rect() { x = 0, y = 0, w = sourceWidth, h = sourceHeight };
            SDL.SDL_Point centerPoint = new SDL.SDL_Point() { x = (int)center.X, y = (int)center.Y };

            int result = SDL.SDL_RenderCopyEx(Handle, texture.Handle, ref sourceRectangle, ref destinationRectangle, angle, ref centerPoint, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderCopyEx"));
            }
        }

        public void RenderTexture(ITexture texture, float positionX, float positionY, int sourceWidth, int sourceHeight)
        {
            Rectangle source = new Rectangle(0, 0, sourceWidth, sourceHeight);
            RenderTexture(texture, positionX, positionY, source);
        }

        public void RenderTexture(ITexture texture, float positionX, float positionY, Rectangle source)
        {
            if (texture.Handle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(texture.Handle));
            }

            int width = source.Width;
            int height = source.Height;

            // SDL only accepts integer positions (x,y) in the rendering Rect
            SDL.SDL_Rect destinationRectangle = new SDL.SDL_Rect() { x = (int)positionX, y = (int)positionY, w = width, h = height };
            SDL.SDL_Rect sourceRectangle = new SDL.SDL_Rect() { x = source.X, y = source.Y, w = width, h = height };

            int result = SDL.SDL_RenderCopy(Handle, texture.Handle, ref sourceRectangle, ref destinationRectangle);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderCopy"));
            }
        }

        public void RenderPresent()
        {
            SDL.SDL_RenderPresent(Handle);
        }

        public void ResetRenderTarget()
        {
            int result = SDL2.SDL.SDL_SetRenderTarget(Handle, IntPtr.Zero);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetRenderTarget"));
            }
        }

        public void SetRenderTarget(ITexture renderTarget)
        {
            if (!flags.Contains(RendererFlags.SupportRenderTargets))
            {
                throw new InvalidOperationException("This renderer does not support render targets. Did you create the renderer with the RendererFlags.SupportRenderTargets flag?");
            }

            int result = SDL2.SDL.SDL_SetRenderTarget(Handle, renderTarget.Handle);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetRenderTarget"));
            }
        }

        public void SetBlendMode(BlendMode blendMode)
        {
            int result = SDL2.SDL.SDL_SetRenderDrawBlendMode(Handle, (SDL2.SDL.SDL_BlendMode)blendMode);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetRenderDrawBlendMode"));
            }
        }

        public void SetDrawColor(byte r, byte g, byte b, byte a)
        {
            int result = SDL.SDL_SetRenderDrawColor(Handle, r, g, b, a);

            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetRenderDrawColor"));
            }
        }

        public void SetRenderLogicalSize(int width, int height)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            int result = SDL2.SDL.SDL_RenderSetLogicalSize(Handle, width, height);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderSetLogicalSize"));
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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