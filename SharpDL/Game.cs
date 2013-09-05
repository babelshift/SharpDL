using SDL2;
using SharpDL.Graphics;
using SharpDL.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		private GameTime gameTime = new GameTime();

		private bool isFrameRateCapped = true;

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

		public event EventHandler<MouseWheelEventArgs> MouseWheelScrolling;
		public event EventHandler<MouseButtonEventArgs> MouseButtonPressed;
		public event EventHandler<MouseButtonEventArgs> MouseButtonReleased;
		public event EventHandler<MouseMotionEventArgs> MouseMoving;
		public event EventHandler<TextInputEventArgs> TextInputting;
		public event EventHandler<TextEditingEventArgs> TextEditing;
		public event EventHandler<KeyboardEventArgs> KeyPressed;
		public event EventHandler<KeyboardEventArgs> KeyReleased;
		public event EventHandler<VideoDeviceSystemEventArgs> VideoDeviceSystemEvent;
		public event EventHandler<QuitEventArgs> Quitting;
		public event EventHandler<EventArgs> Activated;
		public event EventHandler<EventArgs> Deactivated;
		public event EventHandler<EventArgs> Disposed;
		public event EventHandler<EventArgs> Exiting;

		public EventHandler<WindowEventArgs> WindowEvent;

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

		private void RaiseEvent<T>(EventHandler<T> eventHandler, T eventArguments)
			where T : EventArgs
		{
			if (eventHandler != null)
				eventHandler(this, eventArguments);
		}

		#endregion

		#region Game Cycle Control

		public void Run()
		{
			Initialize();
			LoadContent();

			while (!IsExiting)
			{
				SDL.SDL_Event rawEvent = new SDL.SDL_Event();
				while (SDL.SDL_PollEvent(out rawEvent) == 1)
				{
					if (rawEvent.type == SDL.SDL_EventType.SDL_QUIT)
					{
						IsExiting = true;
						break;
					}

					RaiseGameEventFromRawEvent(rawEvent);
				}

				Tick();
			}

			UnloadContent();
		}

		private void RaiseGameEventFromRawEvent(SDL.SDL_Event rawEvent)
		{
			if (rawEvent.type == SDL.SDL_EventType.SDL_FIRSTEVENT)
				return;
			else if (rawEvent.type == SDL.SDL_EventType.SDL_WINDOWEVENT)
			{
				WindowEventArgs eventArgs = GameEventFactory<WindowEventArgs>.CreateGameEvent(rawEvent);
				if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Close)
					RaiseEvent<WindowEventArgs>(Window.Close, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Enter)
					RaiseEvent<WindowEventArgs>(Window.Enter, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Exposed)
					RaiseEvent<WindowEventArgs>(Window.Exposed, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.FocusGained)
					RaiseEvent<WindowEventArgs>(Window.FocusGained, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.FocusLost)
					RaiseEvent<WindowEventArgs>(Window.FocusLost, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Hidden)
					RaiseEvent<WindowEventArgs>(Window.Hidden, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Leave)
					RaiseEvent<WindowEventArgs>(Window.Leave, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Maximized)
					RaiseEvent<WindowEventArgs>(Window.Maximized, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Minimized)
					RaiseEvent<WindowEventArgs>(Window.Minimized, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Moved)
					RaiseEvent<WindowEventArgs>(Window.Moved, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Resized)
					RaiseEvent<WindowEventArgs>(Window.Resized, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Restored)
					RaiseEvent<WindowEventArgs>(Window.Restored, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Shown)
					RaiseEvent<WindowEventArgs>(Window.Shown, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.SizeChanged)
					RaiseEvent<WindowEventArgs>(Window.SizeChanged, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_QUIT)
			{
				QuitEventArgs eventArgs = GameEventFactory<QuitEventArgs>.CreateGameEvent(rawEvent);
				RaiseEvent<QuitEventArgs>(Quitting, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_SYSWMEVENT)
			{
				VideoDeviceSystemEventArgs eventArgs = GameEventFactory<VideoDeviceSystemEventArgs>.CreateGameEvent(rawEvent);
				RaiseEvent<VideoDeviceSystemEventArgs>(VideoDeviceSystemEvent, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_KEYDOWN
				|| rawEvent.type == SDL.SDL_EventType.SDL_KEYUP)
			{
				KeyboardEventArgs eventArgs = GameEventFactory<KeyboardEventArgs>.CreateGameEvent(rawEvent);
				if (eventArgs.State == KeyboardEventArgs.KeyState.Pressed)
					RaiseEvent<KeyboardEventArgs>(KeyPressed, eventArgs);
				else if (eventArgs.State == KeyboardEventArgs.KeyState.Released)
					RaiseEvent<KeyboardEventArgs>(KeyReleased, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_TEXTEDITING)
			{
				TextEditingEventArgs eventArgs = GameEventFactory<TextEditingEventArgs>.CreateGameEvent(rawEvent);
				RaiseEvent<TextEditingEventArgs>(TextEditing, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_TEXTINPUT)
			{
				TextInputEventArgs eventArgs = GameEventFactory<TextInputEventArgs>.CreateGameEvent(rawEvent);
				RaiseEvent<TextInputEventArgs>(TextInputting, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_MOUSEMOTION)
			{
				MouseMotionEventArgs eventArgs = GameEventFactory<MouseMotionEventArgs>.CreateGameEvent(rawEvent);
				RaiseEvent<MouseMotionEventArgs>(MouseMoving, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_MOUSEBUTTONUP
				|| rawEvent.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
			{
				MouseButtonEventArgs eventArgs = GameEventFactory<MouseButtonEventArgs>.CreateGameEvent(rawEvent);

				if (eventArgs.State == MouseButtonEventArgs.MouseButtonState.Pressed)
					RaiseEvent<MouseButtonEventArgs>(MouseButtonPressed, eventArgs);
				else if (eventArgs.State == MouseButtonEventArgs.MouseButtonState.Released)
					RaiseEvent<MouseButtonEventArgs>(MouseButtonReleased, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_MOUSEWHEEL)
			{
				MouseWheelEventArgs eventArgs = GameEventFactory<MouseWheelEventArgs>.CreateGameEvent(rawEvent);
				RaiseEvent<MouseWheelEventArgs>(MouseWheelScrolling, eventArgs);
			}
		}

		private Timer gameTimer = new Timer();
		private TimeSpan accumulatedElapsedTime = TimeSpan.Zero;
		private TimeSpan targetElapsedTime = TimeSpan.FromSeconds(1 / FRAMES_PER_SECOND);

		private void Tick()
		{
			while (isFrameRateCapped && (accumulatedElapsedTime < targetElapsedTime))
			{
				accumulatedElapsedTime += gameTimer.ElapsedTime;
				gameTimer.Stop();
				gameTimer.Start();

				if (isFrameRateCapped && (accumulatedElapsedTime < targetElapsedTime))
				{
					TimeSpan sleepTime = targetElapsedTime - accumulatedElapsedTime;
					SDL.SDL_Delay((UInt32)sleepTime.TotalMilliseconds);
				}
			}

			if (accumulatedElapsedTime > TimeSpan.FromSeconds(0.5))
				accumulatedElapsedTime = TimeSpan.FromSeconds(0.5);

			if (isFrameRateCapped)
			{
				int stepCount = 0;

				while (accumulatedElapsedTime >= targetElapsedTime)
				{
					gameTime.TotalGameTime += targetElapsedTime;
					accumulatedElapsedTime -= targetElapsedTime;
					stepCount++;

					Update(gameTime);
				}

				gameTime.ElapsedGameTime = TimeSpan.FromTicks(targetElapsedTime.Ticks * stepCount);
			}

			Draw(gameTime);
		}

		public void Quit()
		{
			RaiseEvent(Exiting, EventArgs.Empty);
			Dispose();
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

			if (SDL.SDL_Init(flags) != 0)
				throw new Exception(String.Format("SDL_Init: {0}", SDL.SDL_GetError()));

			if (SDL_ttf.TTF_Init() != 0)
				throw new Exception(String.Format("TTF_Init: {0}", SDL.SDL_GetError()));

			int initImageResult = SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);
			if ((initImageResult & (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG) != (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG)
				throw new Exception(String.Format("IMG_Init: {0}", SDL.SDL_GetError()));
		}

		protected virtual void LoadContent()
		{
			//Color color = new Color(255, 165, 0, 255);
			//Font font = new Font("Fonts/lazy.ttf", 28);
			//Surface fontSurface = new Surface(font, ".", color);
			//fpsText = new TrueTypeText(Renderer, fontSurface, ".", font, color);
		}

		//TimeSpan elapsedTime = TimeSpan.Zero;
		//int frameRate = 0;
		//int frameCounter = 0;
		protected virtual void Update(GameTime gameTime)
		{
			//elapsedTime += gameTime.ElapsedGameTime;

			//if (elapsedTime >= TimeSpan.FromSeconds(1))
			//{
			//	elapsedTime -= TimeSpan.FromSeconds(1);
			//	frameRate = frameCounter;
			//	frameCounter = 0;
			//}
		}

		//TrueTypeText fpsText;

		protected virtual void Draw(GameTime gameTime)
		{
			//frameCounter++;

			//string fps = String.Format("FPS: {0}", frameRate);

			//fpsText.UpdateText(fps);

			//Renderer.RenderTexture(fpsText.Texture, 0, 100);

			//Renderer.RenderPresent();
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
			if (Window != null)
				SDL.SDL_DestroyWindow(Window.Handle);

			if (Renderer != null)
				SDL.SDL_DestroyRenderer(Renderer.Handle);

			SDL_ttf.TTF_Quit();
			SDL_image.IMG_Quit();
			SDL.SDL_Quit();
			RaiseEvent(Disposed, EventArgs.Empty);
		}

		#endregion
	}
}
