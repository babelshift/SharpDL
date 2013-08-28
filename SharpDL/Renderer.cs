using SDL2;
using System;
using System.Collections.Generic;

namespace SharpDL
{
	public class Renderer : IDisposable
    {
        public Window Window { get; private set; }
        public int Index { get; private set; }
        public IEnumerable<SDL.SDL_WindowFlags> Flags { get; private set; }
        public IntPtr Handle { get; private set; }

        public Renderer(Window window, int index, SDL.SDL_RendererFlags flags)
        {
            this.Window = window;
            this.Index = index;

            List<SDL.SDL_RendererFlags> copyFlags = new List<SDL.SDL_RendererFlags>();
            foreach (SDL.SDL_RendererFlags flag in Enum.GetValues(typeof(SDL.SDL_RendererFlags)))
                if (flags.HasFlag(flag))
                    copyFlags.Add(flag);

            this.Handle = SDL.SDL_CreateRenderer(this.Window.Handle, this.Index, (uint)flags);
            if (this.Handle == null)
                throw new Exception("SDL_CreateRenderer");
        }

        public void RenderClear()
        {
            SDL.SDL_RenderClear(this.Handle);
        }

        public void RenderTexture(Texture texture, int x, int y)
        {
            SDL.SDL_Rect destinationRectangle = new SDL.SDL_Rect() { x = x, y = y, w = texture.Width, h = texture.Height };
            SDL.SDL_Rect sourceRectangle = new SDL.SDL_Rect() { x = 0, y = 0, w = texture.Width, h = texture.Height };

            SDL.SDL_RenderCopy(this.Handle, texture.Handle, IntPtr.Zero, ref destinationRectangle);
        }

        public void RenderPresent()
        {
            SDL.SDL_RenderPresent(this.Handle);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Renderer()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            SDL.SDL_DestroyRenderer(this.Handle);
        }
    }
}
