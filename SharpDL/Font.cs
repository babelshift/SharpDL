using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public class Font : IDisposable
	{
		public string FilePath { get; private set; }
		public int PointSize { get; private set; }
		public IntPtr Handle { get; private set; }

		public Font(string path, int fontPointSize)
		{
			FilePath = path;
			PointSize = fontPointSize;

			Handle = SDL_ttf.TTF_OpenFont(path, fontPointSize);
			if (Handle == null || Handle == IntPtr.Zero)
				throw new Exception(String.Format("TTF_OpenFont: {0}", SDL.SDL_GetError()));
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Font()
		{
			Dispose(false);
		}

		private void Dispose(bool disposing)
		{
			SDL_ttf.TTF_CloseFont(Handle);
		}
	}
}
