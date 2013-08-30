using System;
using SDL2;
using System.Collections;
using System.Collections.Generic;

namespace SharpDL
{
	public class Window : IDisposable
	{
		[Flags]
		public enum WindowFlags
		{
			WindowShown = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
		}

		public string Title { get; private set; }
		public int X { get; private set; }
		public int Y { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public IEnumerable<Window.WindowFlags> Flags { get; private set; }
		public IntPtr Handle { get; private set; }

		public Window(string title, int x, int y, int width, int height, WindowFlags flags)
		{
			this.Title = title;
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;

			List<Window.WindowFlags> copyFlags = new List<Window.WindowFlags>();
			foreach (Window.WindowFlags flag in Enum.GetValues(typeof(Window.WindowFlags)))
				if (flags.HasFlag(flag))
					copyFlags.Add(flag);

			this.Flags = copyFlags;

			this.Handle = SDL.SDL_CreateWindow(this.Title, this.X, this.Y, this.Width, this.Height, (SDL.SDL_WindowFlags)flags);
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
