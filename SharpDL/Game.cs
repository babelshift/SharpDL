using SDL2;
using SharpDL.Events;
using SharpDL.Graphics;
using SharpDL.Input;
using System;
using System.Collections.Generic;

namespace SharpDL
{
    public abstract class Game : IDisposable
    {
        #region Members

        private const uint EMPTY_UINT = 0;
        private const int EMPTY_INT = -1;
        private const float FRAMES_PER_SECOND = 60f;
        private readonly GameTime gameTime = new GameTime();
        private readonly Timer gameTimer = new Timer();

        private TimeSpan accumulatedElapsedTime = TimeSpan.Zero;
        private TimeSpan targetElapsedTime = TimeSpan.FromSeconds(1 / FRAMES_PER_SECOND);
        private bool isFrameRateCapped = true;

        #endregion Members

        #region Properties

        protected EventManager EventManager { get; private set; }

        protected Window Window { get; private set; }

        protected Renderer Renderer { get; private set; }

        protected bool IsActive { get; private set; }

        protected bool IsExiting { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>Default constructor of the base Game class does nothing. Only when Initialize is called
        /// is anything useful done.
        /// </summary>
        public Game()
        {
            EventManager = new EventManager();
            EventManager.WindowClosed += OnExiting;
            EventManager.Quitting += OnExiting;
        }

        #endregion Constructors

        #region Events

        private void OnExiting(object sender, GameEventArgs e)
        {
            IsExiting = true;
        }

        #endregion Events

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
                {
                    EventManager.HandleEvent(rawEvent);
                }
             
                Tick();
            }

            UnloadContent();
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
            EventManager.HandleExiting(this, EventArgs.Empty);
        }

        #endregion Game Cycle Control

        #region Game Cycle

        /// <summary>Initializes the game by calling initialize on the SDL2 instance with the passed flags
        /// or "EVERYTHING" if 0. Additionally, this method will initialize SDL_ttf and SDL_image to load fonts and images.
        /// </summary>
        /// <param name="types">Bit flags indicating the way in which SDL should be initialized</param>
        protected virtual void Initialize(InitializeType types = InitializeType.Everything)
        {
            if (SDL.SDL_Init((uint)types) != 0)
            {
                throw new InvalidOperationException($"SDL_Init: {SDL.SDL_GetError()}");
            }

            if (SDL_ttf.TTF_Init() != 0)
            {
                throw new InvalidOperationException(String.Format("TTF_Init: {0}", SDL.SDL_GetError()));
            }

            int initImageResult = SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);
            if ((initImageResult & (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG) != (int)SDL_image.IMG_InitFlags.IMG_INIT_PNG)
            {
                throw new InvalidOperationException(String.Format("IMG_Init: {0}", SDL.SDL_GetError()));
            }
        }

        /// <summary>Used for potentially long lasting operations that should only occur relatively rarely. Usually, this
        /// method is used to load images, textures, maps, sounds, videos, and other game assets at the beginning of a level or area.
        /// </summary>
        protected abstract void LoadContent();

        /// <summary>Update the state of the game such as positions, health, entity properties, and more.
        /// This is called before Draw in the main game loop.
        /// </summary>
        /// <param name="gameTime">Allows access to total game time and elapsed game time since the last update</param>
        protected virtual void Update(GameTime gameTime)
        {
            Mouse.UpdateMouseState();

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
            Dispose();
        }

        #endregion Game Cycle

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
            Window = new Window(title, x, y, width, height, flags);
        }

        /// <summary>Creates a SDL Renderer to copy and draw textures to a window
        /// </summary>
        /// <param name="flags">Bit flags indicating the way in which the renderer should be created</param>
        protected void CreateRenderer(RendererFlags flags)
        {
            CreateRenderer(EMPTY_INT, flags);
        }

        /// <summary>Creates a SDL Renderer to copy and draw textures to a window
        /// </summary>
        /// <param name="index">Index of the renderering driver. -1 to choose the first available.</param>
        /// <param name="flags">Bit flags indicating the way in which the renderer should be created</param>
        protected void CreateRenderer(int index, RendererFlags flags)
        {
            if (Window == null)
            {
                throw new InvalidOperationException("Window has not been initialized. You must first create a Window before creating a Renderer.");
            }

            Renderer = new Renderer(this.Window, index, flags);

            SDL2.SDL.SDL_SetHint(SDL2.SDL.SDL_HINT_RENDER_SCALE_QUALITY, "linear");
        }

        #endregion Initializers

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
            {
                Window.Dispose();
            }

            if (Renderer != null)
            {
                Renderer.Dispose();
            }

            SDL_ttf.TTF_Quit();
            SDL_image.IMG_Quit();
            SDL.SDL_Quit();
            EventManager.HandleDisposed(this, EventArgs.Empty);
        }

        #endregion Dispose
    }
}