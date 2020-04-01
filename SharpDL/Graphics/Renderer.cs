using SDL2;
using SharpDL.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SharpDL.Graphics
{
    public class Renderer : IDisposable
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<RendererFlags> flags = new List<RendererFlags>();

        public Window Window { get; private set; }

        public int Index { get; private set; }

        public IEnumerable<RendererFlags> Flags { get { return flags; } }

        public IntPtr Handle { get; private set; }

        public Renderer(Window window, int index, RendererFlags flags)
        {
            if (window == null)
            {
                throw new ArgumentNullException(Errors.E_WINDOW_NULL);
            }

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

            Handle = SDL.SDL_CreateRenderer(Window.Handle, Index, (SDL.SDL_RendererFlags)flags);
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_CreateRenderer"));
            }
        }

        public void ClearScreen()
        {
            ThrowExceptionIfRendererIsNull();

            int result = SDL.SDL_RenderClear(Handle);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderClear"));
            }
        }

        internal void RenderTexture(IntPtr textureHandle, float positionX, float positionY, int sourceWidth, int sourceHeight, double angle, Vector center)
        {
            if(textureHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException("textureHandle", Errors.E_TEXTURE_NULL);
            }

            // SDL only accepts integer positions (x,y) in the rendering Rect
            SDL.SDL_Rect destinationRectangle = new SDL.SDL_Rect() { x = (int)positionX, y = (int)positionY, w = sourceWidth, h = sourceHeight };
            SDL.SDL_Rect sourceRectangle = new SDL.SDL_Rect() { x = 0, y = 0, w = sourceWidth, h = sourceHeight };
            SDL.SDL_Point centerPoint = new SDL.SDL_Point() { x = (int)center.X, y = (int)center.Y };

            int result = SDL.SDL_RenderCopyEx(Handle, textureHandle, ref sourceRectangle, ref destinationRectangle, angle, ref centerPoint, SDL.SDL_RendererFlip.SDL_FLIP_NONE);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderCopyEx"));
            }
        }

        internal void RenderTexture(IntPtr textureHandle, float positionX, float positionY, int sourceWidth, int sourceHeight)
        {
            Rectangle source = new Rectangle(0, 0, sourceWidth, sourceHeight);
            RenderTexture(textureHandle, positionX, positionY, source);
        }

        internal void RenderTexture(IntPtr textureHandle, float positionX, float positionY, Rectangle source)
        {
            if (textureHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException("textureHandle", Errors.E_TEXTURE_NULL);
            }

            int width = source.Width;
            int height = source.Height;

            // SDL only accepts integer positions (x,y) in the rendering Rect
            SDL.SDL_Rect destinationRectangle = new SDL.SDL_Rect() { x = (int)positionX, y = (int)positionY, w = width, h = height };
            SDL.SDL_Rect sourceRectangle = new SDL.SDL_Rect() { x = source.X, y = source.Y, w = width, h = height };

            int result = SDL.SDL_RenderCopy(Handle, textureHandle, ref sourceRectangle, ref destinationRectangle);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderCopy"));
            }
        }

        public void RenderPresent()
        {
            ThrowExceptionIfRendererIsNull();

            SDL.SDL_RenderPresent(Handle);
        }

        public void ResetRenderTarget()
        {
            ThrowExceptionIfRendererIsNull();

            int result = SDL2.SDL.SDL_SetRenderTarget(Handle, IntPtr.Zero);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetRenderTarget"));
            }
        }

        public void SetRenderTarget(RenderTarget renderTarget)
        {
            ThrowExceptionIfRendererIsNull();

            if (!flags.Contains(RendererFlags.SupportRenderTargets))
            {
                throw new InvalidOperationException(Errors.E_RENDERER_NO_RENDER_TARGET_SUPPORT);
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
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetDrawBlendMode"));
            }
        }

        public void SetDrawColor(byte r, byte g, byte b, byte a)
        {
            ThrowExceptionIfRendererIsNull();

            int result = SDL.SDL_SetRenderDrawColor(Handle, r, g, b, a);

            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetRenderDrawColor"));
            }
        }

        public void SetRenderLogicalSize(int width, int height)
        {
            ThrowExceptionIfRendererIsNull();

            int result = SDL2.SDL.SDL_RenderSetLogicalSize(Handle, width, height);
            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderSetLogicalSize"));
            }
        }

        private void ThrowExceptionIfRendererIsNull()
        {
            if(Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Errors.E_RENDERER_NULL);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Renderer()
        {
            //log.Debug("A renderer resource has leaked. Did you forget to dispose the object?");
        }

        private void Dispose(bool disposing)
        {
            if (Handle != IntPtr.Zero)
            {
                SDL.SDL_DestroyRenderer(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }
}