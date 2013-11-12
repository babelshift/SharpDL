using SDL2;
using SharpDL.Events;
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

		private const uint EMPTY_UINT = 0;
		private const int EMPTY_INT = -1;
		private const float FRAMES_PER_SECOND = 60f;
		private TimeSpan accumulatedElapsedTime = TimeSpan.Zero;
		private TimeSpan targetElapsedTime = TimeSpan.FromSeconds(1 / FRAMES_PER_SECOND);

		private bool isFrameRateCapped = true;
		private GameTime gameTime = new GameTime();
		private Timer gameTimer = new Timer();

		#endregion

		#region Properties

		public Window Window { get; private set; }
		public Renderer Renderer { get; private set; }
		public bool IsActive { get; private set; }
		public bool IsExiting { get; private set; }

		#endregion

		#region Constructors

		/// <summary>Default constructor of the base Game class does nothing. Only when Initialize is called 
		/// is anything useful done.
		/// </summary>
		public Game()
		{
			WindowClosed += Game_WindowClosed;
		}

		void Game_WindowClosed(object sender, GameEventArgs e)
		{
			IsExiting = true;
		}

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
		public event EventHandler<WindowEventArgs> WindowShown;
		public event EventHandler<WindowEventArgs> WindowHidden;
		public event EventHandler<WindowEventArgs> WindowExposed;
		public event EventHandler<WindowEventArgs> WindowMoved;
		public event EventHandler<WindowEventArgs> WindowResized;
		public event EventHandler<WindowEventArgs> WindowSizeChanged;
		public event EventHandler<WindowEventArgs> WindowMinimized;
		public event EventHandler<WindowEventArgs> WindowMaximized;
		public event EventHandler<WindowEventArgs> WindowRestored;
		public event EventHandler<WindowEventArgs> WindowEntered;
		public event EventHandler<WindowEventArgs> WindowLeave;
		public event EventHandler<WindowEventArgs> WindowFocusGained;
		public event EventHandler<WindowEventArgs> WindowFocusLost;
		public event EventHandler<WindowEventArgs> WindowClosed;

		/// <summary>Raises the Activated event. Activation occurs when the game gains focus.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		protected virtual void OnActivated(object sender, EventArgs args)
		{
			RaiseEvent(Activated, args);
		}

		/// <summary>Raises the Deactivated event. Deactivation occurs when the game loses focus.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		protected virtual void OnDeactivated(object sender, EventArgs args)
		{
			RaiseEvent(Deactivated, args);
		}

		/// <summary>Raises the Exiting event. Exiting occurs when an unrecoverable exception occurs or the
		/// user directly exits the game.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		protected virtual void OnExiting(object sender, EventArgs args)
		{
			RaiseEvent(Exiting, args);
		}

		/// <summary>Checks if there are any event subscribers prior to raising the event.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="eventHandler"></param>
		/// <param name="eventArguments"></param>
		private void RaiseEvent<T>(EventHandler<T> eventHandler, T eventArguments)
			where T : EventArgs
		{
			if (eventHandler != null)
				eventHandler(this, eventArguments);
		}

		#endregion

		#region Game Cycle Control

		/// <summary>Begins the game by performing the following cycle events in this order: Initialize, LoadContent, 
		/// CheckInputs, Update, Draw, UnloadContent.
		/// </summary>
		public void Run()
		{
			Initialize();
			LoadContent();

			while (!IsExiting)
			{
				SDL.SDL_Event rawEvent = new SDL.SDL_Event();
				while (SDL.SDL_PollEvent(out rawEvent) == 1)
					RaiseGameEventFromRawEvent(rawEvent);

				Tick();
			}

			UnloadContent();
		}

		// <summary>The passed raw SDL_Event object is translated into a SharpDL game object and raised using
		// the appropriate EventHandler.
		// </summary>
		// <param name="rawEvent"></param>
		private void RaiseGameEventFromRawEvent(SDL.SDL_Event rawEvent)
		{
			if (rawEvent.type == SDL.SDL_EventType.SDL_FIRSTEVENT)
				return;
			else if (rawEvent.type == SDL.SDL_EventType.SDL_WINDOWEVENT)
			{
				WindowEventArgs eventArgs = GameEventArgsFactory<WindowEventArgs>.Create(rawEvent);
				if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Close)
					RaiseEvent<WindowEventArgs>(WindowClosed, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Enter)
					RaiseEvent<WindowEventArgs>(WindowEntered, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Exposed)
					RaiseEvent<WindowEventArgs>(WindowExposed, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.FocusGained)
					RaiseEvent<WindowEventArgs>(WindowFocusGained, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.FocusLost)
					RaiseEvent<WindowEventArgs>(WindowFocusLost, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Hidden)
					RaiseEvent<WindowEventArgs>(WindowHidden, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Leave)
					RaiseEvent<WindowEventArgs>(WindowLeave, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Maximized)
					RaiseEvent<WindowEventArgs>(WindowMaximized, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Minimized)
					RaiseEvent<WindowEventArgs>(WindowMinimized, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Moved)
					RaiseEvent<WindowEventArgs>(WindowMoved, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Resized)
					RaiseEvent<WindowEventArgs>(WindowResized, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Restored)
					RaiseEvent<WindowEventArgs>(WindowRestored, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.Shown)
					RaiseEvent<WindowEventArgs>(WindowShown, eventArgs);
				else if (eventArgs.SubEventType == WindowEventArgs.WindowEventType.SizeChanged)
					RaiseEvent<WindowEventArgs>(WindowSizeChanged, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_QUIT)
			{
				QuitEventArgs eventArgs = GameEventArgsFactory<QuitEventArgs>.Create(rawEvent);
				RaiseEvent<QuitEventArgs>(Quitting, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_SYSWMEVENT)
			{
				VideoDeviceSystemEventArgs eventArgs = GameEventArgsFactory<VideoDeviceSystemEventArgs>.Create(rawEvent);
				RaiseEvent<VideoDeviceSystemEventArgs>(VideoDeviceSystemEvent, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_KEYDOWN
				|| rawEvent.type == SDL.SDL_EventType.SDL_KEYUP)
			{
				KeyboardEventArgs eventArgs = GameEventArgsFactory<KeyboardEventArgs>.Create(rawEvent);
				if (eventArgs.State == KeyState.Pressed)
					RaiseEvent<KeyboardEventArgs>(KeyPressed, eventArgs);
				else if (eventArgs.State == KeyState.Released)
					RaiseEvent<KeyboardEventArgs>(KeyReleased, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_TEXTEDITING)
			{
				TextEditingEventArgs eventArgs = GameEventArgsFactory<TextEditingEventArgs>.Create(rawEvent);
				RaiseEvent<TextEditingEventArgs>(TextEditing, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_TEXTINPUT)
			{
				TextInputEventArgs eventArgs = GameEventArgsFactory<TextInputEventArgs>.Create(rawEvent);
				RaiseEvent<TextInputEventArgs>(TextInputting, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_MOUSEMOTION)
			{
				MouseMotionEventArgs eventArgs = GameEventArgsFactory<MouseMotionEventArgs>.Create(rawEvent);
				RaiseEvent<MouseMotionEventArgs>(MouseMoving, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_MOUSEBUTTONUP
				|| rawEvent.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
			{
				MouseButtonEventArgs eventArgs = GameEventArgsFactory<MouseButtonEventArgs>.Create(rawEvent);

				if (eventArgs.State == MouseButtonState.Pressed)
					RaiseEvent<MouseButtonEventArgs>(MouseButtonPressed, eventArgs);
				else if (eventArgs.State == MouseButtonState.Released)
					RaiseEvent<MouseButtonEventArgs>(MouseButtonReleased, eventArgs);
			}
			else if (rawEvent.type == SDL.SDL_EventType.SDL_MOUSEWHEEL)
			{
				MouseWheelEventArgs eventArgs = GameEventArgsFactory<MouseWheelEventArgs>.Create(rawEvent);
				RaiseEvent<MouseWheelEventArgs>(MouseWheelScrolling, eventArgs);
			}
		}

		/// <summary>A tick is equal to a single time step forward in the game state. During each tick, the game will update total game time,
		/// elapsed update time, and frame rates. It is important to note that the implementation is based on a Fixed Time Step algorithm where
		/// each update and draw occur in the same constant fixed intervals. Additionally, the game will call the Update and Draw game cycle 
		/// methods to be overridden by each implementation's specific Game Update and Draw logic. This method is based heavily on MonoGame's 
		/// tick implementation and suggestions from Glenn Fiedler's blog (http://gafferongames.com/game-physics/fix-your-timestep/).
		/// </summary>
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

		/// <summary>Raises the Exiting event and disposes of this instance.
		/// </summary>
		public void Quit()
		{
			IsExiting = true;
			RaiseEvent(Exiting, EventArgs.Empty);
			Dispose();
		}

		#endregion

		#region Game Cycle

		/// <summary>
		/// Initializes the game by calling initialize on the SDL2 instance with "EVERYTHING".
		/// </summary>
		protected virtual void Initialize()
		{
			Initialize(SDL.SDL_INIT_EVERYTHING);
		}

		/// <summary>Initializes the game by calling initialize on the SDL2 instance with the passed flags 
		/// or "EVERYTHING" if 0. Additionally, this method will initialize SDL_ttf and SDL_image to load fonts and images.
		/// </summary>
		/// <param name="flags">Bit flags indicating the way in which SDL should be initialized</param>
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

		/// <summary>Used for potentially long lasting operations that should only occur relatively rarely. Usually, this
		/// method is used to load images, textures, maps, sounds, videos, and other game assets at the beginning of a level or area.
		/// </summary>
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

		/// <summary>Update the state of the game such as positions, health, entity properties, and more. 
		/// This is called before Draw in the main game loop.
		/// </summary>
		/// <param name="gameTime">Allows access to total game time and elapsed game time since the last update</param>
		protected virtual void Update(GameTime gameTime)
		{
			//if (rawEvents.Count > 0)
			//	RaiseGameEventFromRawEvent(rawEvents.Dequeue());

			//elapsedTime += gameTime.ElapsedGameTime;

			//if (elapsedTime >= TimeSpan.FromSeconds(1))
			//{
			//	elapsedTime -= TimeSpan.FromSeconds(1);
			//	frameRate = frameCounter;
			//	frameCounter = 0;
			//}
		}

		//TrueTypeText fpsText;

		/// <summary>Draw the current state of the game such as textures, surfaces, maps, and other visual content. 
		/// This is called after Update in the main game loop.
		/// </summary>
		/// <param name="gameTime">Allows access to total game time and elapsed game time since the last update</param>
		protected virtual void Draw(GameTime gameTime)
		{
			//frameCounter++;

			//string fps = String.Format("FPS: {0}", frameRate);

			//fpsText.UpdateText(fps);

			//Renderer.RenderTexture(fpsText.Texture, 0, 100);

			//Renderer.RenderPresent();
		}

		/// <summary>Used to unload game assets that were loaded during the LoadContent method. Usually, you use this to free
		/// any resources that should not be lingering any longer or are no longer required.
		/// </summary>
		protected virtual void UnloadContent()
		{
		}

		#endregion

		#region Initializers

		/// <summary>Creates a SDL window to render content within.
		/// </summary>
		/// <param name="title">Title of the window</param>
		/// <param name="x">X position of the top left corner</param>
		/// <param name="y">Y position of the top left corner</param>
		/// <param name="width">Width of the window</param>
		/// <param name="height">Height of the window</param>
		/// <param name="flags">Bit flags indicating the way in which the window should be created</param>
		protected void CreateWindow(string title, int x, int y, int width, int height, WindowFlags flags)
		{
			this.Window = new Window(title, x, y, width, height, flags);
		}

		/// <summary>Creates a SDL Renderer to copy and draw textures to a window
		/// </summary>
		/// <param name="flags">Bit flags indicating the way in which the renderer should be created</param>
		protected void CreateRenderer(RendererFlags flags)
		{
			this.CreateRenderer(EMPTY_INT, flags);
		}

		/// <summary>Creates a SDL Renderer to copy and draw textures to a window
		/// </summary>
		/// <param name="index">Index of the renderering driver. -1 to choose the first available.</param>
		/// <param name="flags">Bit flags indicating the way in which the renderer should be created</param>
		protected void CreateRenderer(int index, RendererFlags flags)
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
