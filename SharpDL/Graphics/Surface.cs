using SDL2;
using System;
using System.Runtime.InteropServices;

namespace SharpDL.Graphics
{
    public class Surface : ISurface
    {
        private SafeSurfaceHandle safeHandle;

        public string FilePath { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public SurfaceType Type { get; private set; }

        public IntPtr Handle { get { return safeHandle.DangerousGetHandle(); } }

        internal Surface(string filePath, SurfaceType surfaceType)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            FilePath = filePath;
            Type = surfaceType;

            IntPtr unsafeHandle = SDL_image.IMG_Load(FilePath);

            if (unsafeHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException($"Error while loading image surface: {SDL.SDL_GetError()}");
            }

            safeHandle = new SafeSurfaceHandle(unsafeHandle);

            GetSurfaceMetaData();
        }

        internal Surface(IFont font, string text)
            : this(font, text, Color.Black, 0)
        {
        }

        internal Surface(IFont font, string text, Color color)
            : this(font, text, color, 0)
        {
        }

        internal Surface(IFont font, string text, Color color, int wrapLength)
        {
            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            // Only check null instead of Empty/Whitespace because SDL allows everything but null for rendering fonts.
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (wrapLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(wrapLength), "Wrap length must be greater than or equal to 0.");
            }

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
                throw new InvalidOperationException($"Error while loading text surface: {SDL.SDL_GetError()}");
            }

            safeHandle = new SafeSurfaceHandle(unsafeHandle);

            GetSurfaceMetaData();
        }

        private void GetSurfaceMetaData()
        {
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