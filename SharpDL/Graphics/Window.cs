using Microsoft.Extensions.Logging;
using SDL2;
using System;
using System.Collections.Generic;

namespace SharpDL.Graphics
{
    public class Window : IWindow
    {
        private readonly ILogger<Window> logger;

        public string Title { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public IEnumerable<WindowFlags> Flags { get; private set; }

        public IntPtr Handle { get; private set; }

        internal Window(string title, int x, int y, int width, int height, WindowFlags flags, ILogger<Window> logger = null)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                title = "SharpDL Window";
            }

            if (width < 0)
            {
                width = 0;
            }

            if (height < 0)
            {
                height = 0;
            }

            this.logger = logger;

            Title = title;
            X = x;
            Y = y;
            Width = width;
            Height = height;

            List<WindowFlags> copyFlags = new List<WindowFlags>();
            foreach (WindowFlags flag in Enum.GetValues(typeof(WindowFlags)))
            {
                if (flags.HasFlag(flag))
                {
                    copyFlags.Add(flag);
                }
            }

            Flags = copyFlags;

            Handle = SDL.SDL_CreateWindow(this.Title, this.X, this.Y, this.Width, this.Height, (SDL.SDL_WindowFlags)flags);
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException($"SDL_CreateWindow: {SDL.SDL_GetError()}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Window()
        {
            logger?.LogWarning("A window resource has leaked. Did you forget to dispose the object?");
        }

        private void Dispose(bool isDisposing)
        {
            if (Handle != IntPtr.Zero)
            {
                SDL.SDL_DestroyWindow(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }
}