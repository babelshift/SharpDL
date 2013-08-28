using System;
using SDL2;

namespace SharpDL
{
	public class Surface : IDisposable
    {
        public String FilePath { get; private set; }
        public IntPtr Handle { get; private set; }

        public Surface(String filePath)
        {
            this.FilePath = filePath;

            this.Handle = SDL.SDL_LoadBMP(this.FilePath);
            if (this.Handle == null)
                throw new Exception("SDL_LoadBMP");
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
            SDL.SDL_FreeSurface(this.Handle);
        }
    }
}
