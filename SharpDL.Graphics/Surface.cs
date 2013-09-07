using System;
using SDL2;
using System.Runtime.InteropServices;

namespace SharpDL.Graphics
{
	public class Surface : IDisposable
	{
		public enum SurfaceType
		{
			BMP,
			PNG,
			JPG,
			Text
		}

		public string FilePath { get; private set; }
		public IntPtr Handle { get; private set; }
		public SurfaceType Type { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		//public IntPtr Pixels { get; set; }
		//public int Pitch { get; set; }

		public Surface(string filePath, SurfaceType surfaceType)
		{
			FilePath = filePath;
			Type = surfaceType;

			if (surfaceType == SurfaceType.BMP)
				Handle = SDL.SDL_LoadBMP(FilePath);
			else if (surfaceType == SurfaceType.PNG)
				Handle = SDL_image.IMG_Load(FilePath);

			if (Handle == null || Handle == IntPtr.Zero)
				throw new Exception(String.Format("Error while loading image surface: {0}", SDL.SDL_GetError()));

			GetSurfaceMetaData();
		}

		public Surface(Font font, string text, Color color)
		{
			Type = SurfaceType.Text;
			SDL.SDL_Color rawColor = new SDL.SDL_Color() { r = color.R, g = color.G, b = color.B };

			Handle = SDL_ttf.TTF_RenderText_Solid(font.Handle, text, rawColor);

			if (Handle == null || Handle == IntPtr.Zero)
				throw new Exception(String.Format("Error while loading text surface: {0}", SDL.SDL_GetError()));

			GetSurfaceMetaData();
		}

		private void GetSurfaceMetaData()
		{
			SDL.SDL_Surface rawSurface = (SDL.SDL_Surface)Marshal.PtrToStructure(Handle, typeof(SDL.SDL_Surface));
			Width = rawSurface.w;
			Height = rawSurface.h;
			//Pixels = rawSurface.pixels;
			//Pitch = rawSurface.pitch;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Surface()
		{
			Dispose(false);
		}

		private void Dispose(bool disposing)
		{
			SDL.SDL_FreeSurface(Handle);
		}
	}
}
