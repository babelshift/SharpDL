using System;
using SDL2;
using System.Collections;
using System.Collections.Generic;
using SharpDL.Events;

namespace SharpDL.Graphics
{
	[Flags]
	public enum WindowFlags
	{
		Shown = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN,
		Fullscreen = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN,
		FullscreenDesktop = SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP,
		OpenGL = SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL,
		Hidden = SDL.SDL_WindowFlags.SDL_WINDOW_HIDDEN,
		Borderless = SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS,
		Resizable = SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE,
		Minimized = SDL.SDL_WindowFlags.SDL_WINDOW_MINIMIZED,
		Maximized = SDL.SDL_WindowFlags.SDL_WINDOW_MAXIMIZED,
		GrabbedInputFocus = SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_GRABBED,
		InputFocus = SDL.SDL_WindowFlags.SDL_WINDOW_INPUT_FOCUS,
		MouseFocus = SDL.SDL_WindowFlags.SDL_WINDOW_MOUSE_FOCUS,
		Foreign = SDL.SDL_WindowFlags.SDL_WINDOW_FOREIGN
	}

	public class Window : IDisposable
	{
		public string Title { get; private set; }
		public int X { get; private set; }
		public int Y { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public IEnumerable<WindowFlags> Flags { get; private set; }
		public IntPtr Handle { get; private set; }

		public Window(string title, int x, int y, int width, int height, WindowFlags flags)
		{
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
			if (Handle == null || Handle == IntPtr.Zero)
				throw new Exception(String.Format("SDL_CreateWindow: {0}", SDL.SDL_GetError()));
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool isDisposing)
		{
			SDL.SDL_DestroyWindow(Handle);
		}
	}
}
