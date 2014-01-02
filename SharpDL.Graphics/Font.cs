using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public class Font : IDisposable
	{
		//private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public string FilePath { get; private set; }

		public int PointSize { get; private set; }

		public IntPtr Handle { get; private set; }

		public int OutlineSize { get; private set; }

		private bool IsDisposed { get; set; }

		public Font(string path, int fontPointSize)
		{
			FilePath = path;
			PointSize = fontPointSize;

			Handle = SDL_ttf.TTF_OpenFont(path, fontPointSize);
			if (Handle == IntPtr.Zero)
				throw new Exception(String.Format("TTF_OpenFont: {0}", SDL.SDL_GetError()));

			IsDisposed = false;
		}

		public void SetOutlineSize(int outlineSize)
		{
			SDL_ttf.TTF_SetFontOutline(Handle, outlineSize);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Font()
		{
			//log.Debug("A font resource has leaked. Did you forget to dispose the object?");
		}

		private void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				SDL_ttf.TTF_CloseFont(Handle);
				IsDisposed = true;
			}
		}
	}
}
