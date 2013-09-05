using SDL2;
using System;
using System.Collections.Generic;

namespace SharpDL.Graphics
{
	public class Renderer : IDisposable
	{
		[Flags]
		public enum RendererFlags
		{
			RendererAccelerated = SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED,
			RendererPresentVSync = SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
		}

		public Window Window { get; private set; }
		public int Index { get; private set; }
		public IEnumerable<Renderer.RendererFlags> Flags { get; private set; }
		public IntPtr Handle { get; private set; }

		public Renderer(Window window, int index, RendererFlags flags)
		{
			Window = window;
			Index = index;

			List<Renderer.RendererFlags> copyFlags = new List<Renderer.RendererFlags>();
			foreach (Renderer.RendererFlags flag in Enum.GetValues(typeof(Renderer.RendererFlags)))
				if (flags.HasFlag(flag))
					copyFlags.Add(flag);

			Handle = SDL.SDL_CreateRenderer(this.Window.Handle, this.Index, (uint)flags);
			if (Handle == null || Handle == IntPtr.Zero)
				throw new Exception(String.Format("SDL_CreateRenderer: {0}", SDL.SDL_GetError()));
		}

		public void ClearScreen()
		{
			SDL.SDL_RenderClear(Handle);
		}

		public void RenderTexture(Texture texture, int x, int y)
		{
			SDL.SDL_Rect destinationRectangle = new SDL.SDL_Rect() { x = x, y = y, w = texture.Width, h = texture.Height };
			SDL.SDL_Rect sourceRectangle = new SDL.SDL_Rect() { x = 0, y = 0, w = texture.Width, h = texture.Height };

			SDL.SDL_RenderCopy(Handle, texture.Handle, IntPtr.Zero, ref destinationRectangle);
		}

		public void RenderPresent()
		{
			SDL.SDL_RenderPresent(Handle);
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
			SDL.SDL_DestroyRenderer(Handle);
		}
	}
}
