using SDL2;
using System;
using System.Collections.Generic;

namespace SharpDL.Graphics
{
	[Flags]
	public enum RendererFlags
	{
		RendererAccelerated = SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED,
		RendererPresentVSync = SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
	}

	public class Renderer : IDisposable
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public Window Window { get; private set; }

		public int Index { get; private set; }

		public IEnumerable<RendererFlags> Flags { get; private set; }

		public IntPtr Handle { get; private set; }

		public Renderer(Window window, int index, RendererFlags flags)
		{
			Window = window;
			Index = index;

			List<RendererFlags> copyFlags = new List<RendererFlags>();
			foreach (RendererFlags flag in Enum.GetValues(typeof(RendererFlags)))
				if (flags.HasFlag(flag))
					copyFlags.Add(flag);

			Handle = SDL.SDL_CreateRenderer(this.Window.Handle, this.Index, (uint)flags);
			if (Handle == IntPtr.Zero)
				throw new Exception(String.Format("SDL_CreateRenderer: {0}", SDL.SDL_GetError()));
		}

		public void ClearScreen()
		{
			SDL.SDL_RenderClear(Handle);
		}

		public void RenderTexture(Texture texture, float positionX, float positionY)
		{
			Rectangle source = new Rectangle(0, 0, texture.Width, texture.Height);
			RenderTexture(texture, positionX, positionY, source);
		}

		public void RenderTexture(Texture texture, float positionX, float positionY, Rectangle source)
		{
			int width = texture.Width;
			int height = texture.Height;

			if (source.Width > 0 && source.Height > 0)
			{
				width = source.Width;
				height = source.Height;
			}

			// SDL only accepts integer positions (x,y) in the rendering Rect
			SDL.SDL_Rect destinationRectangle = new SDL.SDL_Rect() { x = (int)positionX, y = (int)positionY, w = width, h = height };
			SDL.SDL_Rect sourceRectangle = new SDL.SDL_Rect() { x = source.X, y = source.Y, w = source.Width, h = source.Height };

            if (texture != null)
            {
                if (texture.Handle != IntPtr.Zero)
                {
                    int result = SDL.SDL_RenderCopy(Handle, texture.Handle, ref sourceRectangle, ref destinationRectangle);
                    if (result != 0)
                        throw new Exception(String.Format("SDL_RenderCopy: {0}", SDL.SDL_GetError()));
                }
                else 
                    throw new Exception("Attempted to draw a texture with a null Handle. Maybe it was instantiated incorrectly or disposed?");
            }
            else
                throw new Exception("Attempted to draw a null texture. Maybe it was instantiated incorrectly or disposed?");
		}

		public void RenderPresent()
		{
			SDL.SDL_RenderPresent(Handle);
		}

		public void SetDrawColor(byte r, byte g, byte b, byte a)
		{
			int result = SDL.SDL_SetRenderDrawColor(Handle, r, g, b, a);

			if(result < 0)
				throw new Exception(String.Format("SDL_SetRenderDrawColor: {0}", SDL.SDL_GetError()));
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Renderer()
		{
			log.Debug("A renderer resource has leaked. Did you forget to dispose the object?");
		}

		private void Dispose(bool disposing)
		{
			SDL.SDL_DestroyRenderer(Handle);
		}
	}
}
