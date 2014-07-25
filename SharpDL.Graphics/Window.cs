using SDL2;
using System;
using System.Collections.Generic;

namespace SharpDL.Graphics
{
    public class Window : IDisposable
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string Title { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public IEnumerable<WindowFlags> Flags { get; private set; }

        public IntPtr Handle { get; private set; }

        public Window(string title, int x, int y, int width, int height, WindowFlags flags)
        {
            if (String.IsNullOrEmpty(title))
            {
                title = "SharpDL Window";
            }

            Title = title;
            X = x;
            Y = y;
            Width = width;
            Height = height;

            List<WindowFlags> copyFlags = new List<WindowFlags>();
            foreach (WindowFlags flag in Enum.GetValues(typeof(WindowFlags)))
                if (flags.HasFlag(flag))
                    copyFlags.Add(flag);

            Flags = copyFlags;

            Handle = SDL.SDL_CreateWindow(this.Title, this.X, this.Y, this.Width, this.Height, (SDL.SDL_WindowFlags)flags);
            if (Handle == IntPtr.Zero)
                throw new Exception(String.Format("SDL_CreateWindow: {0}", SDL.SDL_GetError()));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Window()
        {
            //log.Debug("A window resource has leaked. Did you forget to dispose the object?");
        }

        private void Dispose(bool isDisposing)
        {
            SDL.SDL_DestroyWindow(Handle);
        }
    }
}