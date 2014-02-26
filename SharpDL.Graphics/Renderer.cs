using SDL2;
using SharpDL.Shared;
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

	[Flags]
	public enum BlendMode
	{
		None = SDL.SDL_BlendMode.SDL_BLENDMODE_NONE,
		Blend = SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND,
		Add = SDL.SDL_BlendMode.SDL_BLENDMODE_ADD,
		Mod = SDL.SDL_BlendMode.SDL_BLENDMODE_MOD,
	}

	public class Renderer : IDisposable
	{
		//private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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

			Handle = SDL.SDL_CreateRenderer(Window.Handle, Index, (uint)flags);
			if (Handle == IntPtr.Zero)
				throw new Exception(Utilities.GetErrorMessage("SDL_CreateRenderer"));
		}

		public void ClearScreen()
		{
			int result = SDL.SDL_RenderClear(Handle);
			if (Utilities.IsError(result))
				throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderClear"));
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
					if (Utilities.IsError(result))
						throw new Exception(Utilities.GetErrorMessage("SDL_RenderCopy: {0}"));
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

		public void ResetRenderTarget()
		{
			int result = SDL2.SDL.SDL_SetRenderTarget(Handle, IntPtr.Zero);
			if (Utilities.IsError(result))
				throw new Exception(Utilities.GetErrorMessage("SDL_SetRenderTarget: {0}"));
		}

		public void SetRenderTarget(Texture texture)
		{
			if (texture.AccessMode != TextureAccessMode.Target)
				throw new InvalidOperationException("Texture cannot be used as a render target unless AccessMode is set to Target.");

			int result = SDL2.SDL.SDL_SetRenderTarget(Handle, texture.Handle);
			if (Utilities.IsError(result))
				throw new Exception(Utilities.GetErrorMessage("SDL_SetRenderTarget"));
		}

		public void SetBlendMode(BlendMode blendMode)
		{
			int result = SDL2.SDL.SDL_SetRenderDrawBlendMode(Handle, (SDL2.SDL.SDL_BlendMode)blendMode);
			if (Utilities.IsError(result))
				throw new Exception(Utilities.GetErrorMessage("SDL_SetDrawBlendMode: {0}"));
		}

		public void SetDrawColor(byte r, byte g, byte b, byte a)
		{
			int result = SDL.SDL_SetRenderDrawColor(Handle, r, g, b, a);

			if (Utilities.IsError(result))
				throw new Exception(Utilities.GetErrorMessage("SDL_SetRenderDrawColor: {0}"));
		}

		public void SetTextureColorMod(Texture texture, byte r, byte g, byte b)
		{
			int result = SDL.SDL_SetTextureColorMod(texture.Handle, r, g, b);

			if (Utilities.IsError(result))
				throw new Exception(Utilities.GetErrorMessage("SDL_SetTextureColorMod: {0}"));
		}

		public void SetRenderLogicalSize(int width, int height)
		{
			int result = SDL2.SDL.SDL_RenderSetLogicalSize(Handle, width, height);
			if (Utilities.IsError(result))
				throw new Exception(Utilities.GetErrorMessage("SDL_RenderSetLogicalSize: {0}"));
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
			SDL.SDL_DestroyRenderer(Handle);
		}
	}
}