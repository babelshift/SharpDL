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
		#region Members

		private const int EMPTY_UINT = 0;
		private const int EMPTY_INT = -1;

		private const float FRAMES_PER_SECOND = 60f;
		private TimeSpan targetElapsedTime = TimeSpan.FromSeconds(1 / FRAMES_PER_SECOND);

		private GameTime gameTime = new GameTime();

		private bool isFrameRateCapped = true;
		private Timer gameTimer = new Timer();

		private TimeSpan lastTickEndTime;

		#endregion

		#region Properties

		public Window Window { get; private set; }
		public Renderer Renderer { get; private set; }
		public bool IsActive { get; private set; }
		internal bool IsExiting { get; set; }

		#endregion

		#region Constructors

		public Game() { }

		#endregion

		#region Events

		public event EventHandler<EventArgs> Activated;
		public event EventHandler<EventArgs> Deactivated;
		public event EventHandler<EventArgs> Disposed;
		public event EventHandler<EventArgs> Exiting;

		protected virtual void OnActivated(object sender, EventArgs args)
		{
			RaiseEvent(Activated, args);
		}

		protected virtual void OnDeactivated(object sender, EventArgs args)
		{
			RaiseEvent(Deactivated, args);
		}

		protected virtual void OnExiting(object sender, EventArgs args)
		{
			RaiseEvent(Exiting, args);
		}

		#endregion

		#region Game Cycle Control

		public void Run()
		{
			while (!IsExiting)
			{
				SDL.SDL_Event gameEvent = new SDL.SDL_Event();
				while (SDL.SDL_PollEvent(out gameEvent) == 1)
				{
					if (gameEvent.type == SDL.SDL_EventType.SDL_QUIT)
					{
						IsExiting = true;
						break;
					}

					Tick(gameEvent);
				}
			}
		}

		private void Tick(SDL.SDL_Event gameEvent)
		{
			// get the elapsed time since the last tick
			gameTime.TotalGameTime = TimeSpan.FromMilliseconds(SDL.SDL_GetTicks());
			gameTime.ElapsedGameTime = gameTime.TotalGameTime - lastTickEndTime;

			// start timer to determine how long the tick takes
			gameTimer.Start();

			Update(gameTime);

			Draw(gameTime);

			// if this tick was faster than our minimum, delay
			if (isFrameRateCapped && (gameTimer.TotalTime.TotalMilliseconds < (1000 / FRAMES_PER_SECOND)))
			{
				UInt32 millisecondsToDelay = (UInt32)((1000 / FRAMES_PER_SECOND) - gameTimer.TotalTime.TotalMilliseconds);
				SDL.SDL_Delay(millisecondsToDelay);
			}

			gameTimer.Stop();

			// reset the elapsed time because we are done with the tick
			gameTime.ElapsedGameTime = TimeSpan.Zero;

			// record the time at which this tick ended
			lastTickEndTime = TimeSpan.FromMilliseconds(SDL.SDL_GetTicks());
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

		#endregion

		#region Game Cycle

		protected virtual void Initialize()
		{
			Initialize(SDL.SDL_INIT_EVERYTHING);
		}

		protected virtual void Initialize(uint flags)
		{
			if (flags == EMPTY_UINT)
				flags = SDL.SDL_INIT_EVERYTHING;

			if (SDL.SDL_Init(flags) != EMPTY_UINT)
				throw new Exception(String.Format("SDL_Init: {0}", SDL.SDL_GetError()));
		}

		protected virtual void LoadContent()
		{
		}

		protected virtual void Update(GameTime gameTime)
		{

		}

		protected virtual void Draw(GameTime gameTime)
		{
		}

		protected virtual void UnloadContent()
		{
		}

		#endregion

		#region Initializers

		protected void CreateWindow(string title, int x, int y, int width, int height, Window.WindowFlags flags)
		{
			this.Window = new Window(title, x, y, width, height, flags);
		}

		protected void CreateRenderer(Renderer.RendererFlags flags)
		{
			this.CreateRenderer(EMPTY_INT, flags);
		}

		protected void CreateRenderer(int index, Renderer.RendererFlags flags)
		{
			if (this.Window == null)
				throw new Exception("Window has not been initialized. You must first create a Window before creating a Renderer.");

			if (this.Window.Handle == IntPtr.Zero)
				throw new Exception("Window has been initialized, but the handle to the SDL_Window is null. Maybe SDL_CreateWindow failed?");

			this.Renderer = new Renderer(this.Window, index, flags);
		}

		#endregion

		#region General

		private void RaiseEvent<T>(EventHandler<T> eventHandler, T eventArguments)
			where T : EventArgs
		{
			if (eventHandler != null)
				eventHandler(this, eventArguments);
		}

		public TimeSpan TotalGameTime()
		{
			return TimeSpan.FromMilliseconds((double)SDL.SDL_GetTicks());
		}

		#endregion

		#region Dispose

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
			RaiseEvent(Disposed, EventArgs.Empty);
		}

		#endregion
	}
}
