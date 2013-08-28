using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpDL
{
    public class Game : IDisposable
	{
		private static int EMPTY_INT = -1;

		public Window Window { get; private set; }
		public Renderer Renderer { get; private set; }
		public bool IsActive { get; private set; }

        public Game()
            : this(SDL.SDL_INIT_EVERYTHING)
        {
        }

        public Game(uint flags)
        {
            if (SDL.SDL_Init(flags) != 0)
                throw new Exception(String.Format("SDL_Init: {0}", SDL.SDL_GetError()));
        }

		protected void CreateWindow(string title, int x, int y, int width, int height, SDL.SDL_WindowFlags flags)
		{
			this.Window = new Window(title, x, y, width, height, flags);
		}

		protected void CreateRenderer(SDL.SDL_RendererFlags flags)
		{
			this.CreateRenderer(EMPTY_INT, flags);
		}

		protected void CreateRenderer(int index, SDL.SDL_RendererFlags flags)
		{
			if (this.Window == null)
				throw new Exception("Window has not been initialized. You must first create a Window before creating a Renderer.");

			if (this.Window.Handle == IntPtr.Zero)
				throw new Exception("Window has been initialized, but the handle to the SDL_Window is null. Maybe SDL_CreateWindow failed?");

			this.Renderer = new Renderer(this.Window, index, flags);
		}

		public void Run()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void Draw()
		{
		}

        public void Quit()
        {
            SDL.SDL_Quit();
        }

        public void Sleep(TimeSpan delayTime)
        {
            Thread.Sleep(delayTime);
            //SDL.SDL_Delay((uint)delayTime.TotalMilliseconds);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Game()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
			SDL.SDL_DestroyWindow(this.Window.Handle);
			SDL.SDL_DestroyRenderer(this.Renderer.Handle);
        }
    }
}
