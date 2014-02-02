﻿﻿using SDL2;
using System;

namespace SharpDL.Graphics
{
	public enum TextureAccessMode
	{
		Static = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC,
		Streaming = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING
	}

	public class Texture : IDisposable
	{
		//private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public string FilePath { get; private set; }

		public uint PixelFormat { get; private set; }

		public int Access { get; private set; }

		public int Width { get; private set; }

		public int Height { get; private set; }

		public Surface Surface { get; private set; }

		public Renderer Renderer { get; private set; }

		public IntPtr Handle { get; private set; }

		public TextureAccessMode AccessMode { get; private set; }

		public Texture(Renderer renderer, Surface surface)
			: this(renderer, surface, TextureAccessMode.Static)
		{
		}

		public Texture(Renderer renderer, Surface surface, TextureAccessMode accessMode)
		{
			FilePath = surface.FilePath;
			Renderer = renderer;
			Surface = surface;
			AccessMode = accessMode;

			CreateTextureAndCleanup();
		}

		public void UpdateSurfaceAndTexture(Surface surface)
		{
			SDL.SDL_DestroyTexture(Handle);
			Handle = IntPtr.Zero;
			Surface = surface;

			CreateTextureAndCleanup();
		}

		private void CreateTextureAndCleanup()
		{
			bool success = CreateTexture();

			if (!success)
				throw new Exception(String.Format("SDL_CreateTextureFromSurface / SDL_CreateTexture: {0}", SDL.SDL_GetError()));
			
			CleanupAndQueryTexture();
		}

		private bool CreateTexture()
		{
			bool success = false;

			if (Surface == null) return success;

			if (Surface.Handle == IntPtr.Zero) return success;
			
			if (AccessMode == TextureAccessMode.Static)
				Handle = SDL.SDL_CreateTextureFromSurface(Renderer.Handle, Surface.Handle);
			else if (AccessMode == TextureAccessMode.Streaming)
				Handle = SDL.SDL_CreateTexture(Renderer.Handle, SDL.SDL_PIXELFORMAT_ARGB8888, (int)AccessMode, Surface.Width, Surface.Height);

			if (Handle != IntPtr.Zero)
				success = true;

			return success;
		}

		private void CleanupAndQueryTexture()
		{
			if (AccessMode == TextureAccessMode.Static)
				Surface.Dispose();

			uint format;
			int access, width, height;
			SDL.SDL_QueryTexture(Handle, out format, out access, out width, out height);

			PixelFormat = format;
			Access = access;
			Width = width;
			Height = height;
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
			if (AccessMode == TextureAccessMode.Streaming)
				if (Surface != null)
					SDL.SDL_FreeSurface(Surface.Handle);

			SDL.SDL_DestroyTexture(Handle);

			Handle = IntPtr.Zero;
		}
	}
}