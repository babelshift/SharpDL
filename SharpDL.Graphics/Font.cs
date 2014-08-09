using SDL2;
using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{
    public class Font : IDisposable
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string FilePath { get; private set; }

        public int PointSize { get; private set; }

        public IntPtr Handle { get; private set; }

        public int OutlineSize { get; private set; }

        public Font(string path, int fontPointSize)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            FilePath = path;
            PointSize = fontPointSize;

            Handle = SDL_ttf.TTF_OpenFont(path, fontPointSize);
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(String.Format("TTF_OpenFont: {0}", SDL.SDL_GetError()));
            }
        }

        public void SetOutlineSize(int outlineSize)
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Errors.E_FONT_NULL);
            }

            SDL_ttf.TTF_SetFontOutline(Handle, outlineSize);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(Handle != IntPtr.Zero)
            {
                SDL_ttf.TTF_CloseFont(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }
}