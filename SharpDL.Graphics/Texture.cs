﻿using SDL2;
using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{
	public class Texture : ITexture
	{
		//private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		private Renderer renderer;

		public string FilePath { get; private set; }

		public uint PixelFormat { get; private set; }

		public int Access { get; private set; }

		public int Width { get; private set; }

		public int Height { get; private set; }

		public Surface Surface { get; private set; }

		public IntPtr Handle { get; private set; }

		public TextureAccessMode AccessMode { get; private set; }

		public Texture(Renderer renderer, Surface surface)
		{
			this.renderer = renderer;
			FilePath = surface.FilePath;
			AccessMode = TextureAccessMode.Static;
			Surface = surface;

			CreateTextureAndCleanup(surface.Width, surface.Height);
		}

		public void UpdateSurfaceAndTexture(Surface surface)
		{
			SDL.SDL_DestroyTexture(Handle);
			Handle = IntPtr.Zero;
			Surface = surface;

			CreateTextureAndCleanup(surface.Width, surface.Height);
		}

		private void CreateTextureAndCleanup(int width, int height)
		{
			bool success = CreateTexture(width, height);

			if (!success)
				throw new Exception(Utilities.GetErrorMessage("SDL_CreateTextureFromSurface"));

			CleanupAndQueryTexture();
		}

		private bool CreateTexture(int width, int height)
		{
			bool success = false;

			if (Surface == null) return success;

			if (Surface.Handle == IntPtr.Zero) return success;

			Handle = SDL.SDL_CreateTextureFromSurface(renderer.Handle, Surface.Handle);

			if (Handle != IntPtr.Zero)
				success = true;

			return success;
		}

		private void CleanupAndQueryTexture()
		{
			Surface.Dispose();

			uint format;
			int access, width, height;
			SDL.SDL_QueryTexture(Handle, out format, out access, out width, out height);

			PixelFormat = format;
			Access = access;
			Width = width;
			Height = height;
		}

		public void SetBlendMode(BlendMode blendMode)
		{
			int result = SDL2.SDL.SDL_SetTextureBlendMode(Handle, (SDL.SDL_BlendMode)blendMode);
			if (Utilities.IsError(result))
				throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetTextureBlendMode"));
		}

		public void Draw(int x, int y, Rectangle sourceBounds)
		{
			renderer.RenderTexture(Handle, x, y, sourceBounds);
		}

		public void Draw(float x, float y, Rectangle sourceBounds)
		{
			Draw((int)x, (int)y, sourceBounds);
		}

		public void Draw(int x, int y)
		{
			renderer.RenderTexture(Handle, x, y, Width, Height);
		}

		public void Draw(float x, float y)
		{
			Draw((int)x, (int)y);
		}

		public void SetColorMod(byte r, byte g, byte b)
		{
			int result = SDL.SDL_SetTextureColorMod(Handle, r, g, b);

			if (Utilities.IsError(result))
				throw new Exception(Utilities.GetErrorMessage("SDL_SetTextureColorMod: {0}"));
		}

		//public void LockTexture(Surface surface)
		//{
		//	if (AccessMode == TextureAccessMode.Static)
		//	{
		//		throw new Exception("Cannot lock static textures. If you need to lock this texture, instantiate it with TextureAccessMode.Streaming.");
		//	}
		//	else if (AccessMode == TextureAccessMode.Streaming)
		//	{
		//		IntPtr pixels = IntPtr.Zero;
		//		int pitch = 0;
		//		int result = SDL.SDL_LockTexture(Handle, IntPtr.Zero, out pixels, out pitch);
		//		if (result < 0)
		//			throw new Exception(String.Format("SDL_LockTexture: {0}", SDL.SDL_GetError()));
		//		surface.Pixels = pixels;
		//		surface.Pitch = pitch;
		//	}
		//}
		//public void UpdateTexture(Surface surface)
		//{
		//}

		public virtual void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (Surface != null)
				if(Surface.Handle != IntPtr.Zero)
					SDL.SDL_FreeSurface(Surface.Handle);

			SDL.SDL_DestroyTexture(Handle);

			Handle = IntPtr.Zero;
		}
	}
}