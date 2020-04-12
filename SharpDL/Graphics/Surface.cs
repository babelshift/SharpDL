using SDL2;
using SharpDL.Shared;
using System;
using System.Runtime.InteropServices;

namespace SharpDL.Graphics
{

    public class Surface : IDisposable
    {
        private SafeSurfaceHandle safeHandle;

        public string FilePath { get; private set; }

        public IntPtr Handle { get { return safeHandle.DangerousGetHandle(); } }

        public SurfaceType Type { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Surface(string filePath, SurfaceType surfaceType)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath", Errors.E_SURFACE_PATH_MISSING);
            }

            FilePath = filePath;
            Type = surfaceType;

            IntPtr unsafeHandle = IntPtr.Zero;

            if (surfaceType == SurfaceType.BMP)
            {
                unsafeHandle = SDL.SDL_LoadBMP(FilePath);
            }
            else if (surfaceType == SurfaceType.PNG)
            {
                unsafeHandle = SDL_image.IMG_Load(FilePath);
            }

            if (unsafeHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException(String.Format("Error while loading image surface: {0}", SDL.SDL_GetError()));
            }

            safeHandle = new SafeSurfaceHandle(unsafeHandle);

            GetSurfaceMetaData();
        }

        public Surface(Font font, string text, Color color)
            : this(font, text, color, 0)
        {
        }

        public Surface(Font font, string text, Color color, int wrapLength)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font", Errors.E_FONT_NULL);
            }

            if (wrapLength < 0)
                throw new ArgumentOutOfRangeException("wrapLength", "Wrap length must be greater than 0.");

            Type = SurfaceType.Text;
            SDL.SDL_Color rawColor = new SDL.SDL_Color() { r = color.R, g = color.G, b = color.B };

            IntPtr unsafeHandle = IntPtr.Zero;

            if (wrapLength > 0)
            {
                unsafeHandle = SDL_ttf.TTF_RenderText_Blended_Wrapped(font.Handle, text, rawColor, (uint)wrapLength);
            }
            else
            {
                unsafeHandle = SDL_ttf.TTF_RenderText_Blended(font.Handle, text, rawColor);
            }

            if (unsafeHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException(String.Format("Error while loading text surface: {0}", SDL.SDL_GetError()));
            }

            safeHandle = new SafeSurfaceHandle(unsafeHandle);

            GetSurfaceMetaData();
        }

        private void GetSurfaceMetaData()
        {
            if (Handle == IntPtr.Zero)
            {
                throw new InvalidOperationException(Errors.E_FONT_NULL);
            }

            SDL.SDL_Surface rawSurface = (SDL.SDL_Surface)Marshal.PtrToStructure(Handle, typeof(SDL.SDL_Surface));
            Width = rawSurface.w;
            Height = rawSurface.h;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                safeHandle.Dispose();
            }
        }
    }
}