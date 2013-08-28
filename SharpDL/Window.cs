using System;
using SDL2;
using System.Collections;
using System.Collections.Generic;

namespace SharpDL
{
    public class Window : IDisposable
    {
        public string Title { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public IEnumerable<SDL.SDL_WindowFlags> Flags { get; private set; }
        public IntPtr Handle { get; private set; }

        public Window(string title, int x, int y, int width, int height, SDL.SDL_WindowFlags flags)
        {
            this.Title = title;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;

            List<SDL.SDL_WindowFlags> copyFlags = new List<SDL.SDL_WindowFlags>();
            foreach (SDL.SDL_WindowFlags flag in Enum.GetValues(typeof(SDL.SDL_WindowFlags)))
                if (flags.HasFlag(flag))
                    copyFlags.Add(flag);

			this.Flags = copyFlags;

            this.Handle = SDL.SDL_CreateWindow(this.Title, this.X, this.Y, this.Width, this.Height, flags);
            if (this.Handle == null)
                throw new Exception("SDL_CreateWindow");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Window()
        {
            Dispose(false);
        }

        private void Dispose(bool isDisposing)
        {
            SDL.SDL_DestroyWindow(this.Handle);
        }
    }
}
