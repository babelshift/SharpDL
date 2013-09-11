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

		public void RenderTexture(Texture texture, int positionX, int positionY)
		{
			Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);
			RenderTexture(texture, positionX, positionY, source);
		}

		public void RenderTexture(Texture texture, int positionX, int positionY, Rectangle source)
		{
			int width = texture.Width;
			int height = texture.Height;

			if (source.Width > 0 && source.Height > 0)
			{
				width = source.Width;
				height = source.Height;
			}

			SDL.SDL_Rect destinationRectangle = new SDL.SDL_Rect() { x = positionX, y = positionY, w = width, h = height };
			SDL.SDL_Rect sourceRectangle = new SDL.SDL_Rect() { x = source.X, y = source.Y, w = source.Width, h = source.Height };

			int result = SDL.SDL_RenderCopy(Handle, texture.Handle, ref sourceRectangle, ref destinationRectangle);
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
