﻿using SDL2;
using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{
	public enum TextureAccessMode
	{
		Static = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC,
		Streaming = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,
		Target = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET
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
		{
			FilePath = surface.FilePath;
			Renderer = renderer;
			AccessMode = TextureAccessMode.Static;
			Surface = surface;

			CreateTextureAndCleanup(surface.Width, surface.Height);
		}

		/// <summary>
		/// Surface is only used when access mode is Static.
		/// </summary>
		/// <param name="renderer"></param>
		/// <param name="surface"></param>
		/// <param name="accessMode"></param>
		public Texture(Renderer renderer, TextureAccessMode accessMode, int width, int height)
		{

			if (accessMode == TextureAccessMode.Static)
				throw new ArgumentException("Use the constructor that takes a surface when using static texture access mode.", "accessMode");

			Renderer = renderer;
			AccessMode = accessMode;

			CreateTextureAndCleanup(width, height);
		}

		public void UpdateSurfaceAndTexture(Surface surface)
		{
			SDL.SDL_DestroyTexture(Handle);
			Handle = IntPtr.Zero;
			Surface = surface;

			CreateTextureAndCleanup(surface.Width, surface.Height);
		}

		private void CreateTextureAndCleanup(int w, int h)
		{
			bool success = CreateTexture(w, h);

			if (!success)
				throw new Exception(String.Format("SDL_CreateTextureFromSurface / SDL_CreateTexture: {0}", SDL.SDL_GetError()));

			CleanupAndQueryTexture();
		}

		private bool CreateTexture(int w, int h)
		{
			bool success = false;

			if (AccessMode == TextureAccessMode.Static)
			{
				if (Surface == null) return success;

				if (Surface.Handle == IntPtr.Zero) return success;

				Handle = SDL.SDL_CreateTextureFromSurface(Renderer.Handle, Surface.Handle);
			}
			else if (AccessMode == TextureAccessMode.Streaming || AccessMode == TextureAccessMode.Target)
				Handle = SDL.SDL_CreateTexture(Renderer.Handle, SDL.SDL_PIXELFORMAT_RGBA8888, (int)AccessMode, w, h);

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

		public void SetBlendMode(BlendMode blendMode)
		{
			int result = SDL2.SDL.SDL_SetTextureBlendMode(Handle, (SDL.SDL_BlendMode)blendMode);
			if (Utilities.IsError(result))
				throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_SetTextureBlendMode"));
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