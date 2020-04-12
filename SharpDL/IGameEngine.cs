using System;
using SharpDL.Events;
using SharpDL.Graphics;

namespace SharpDL
{
    public interface IGameEngine : IDisposable
    {
        /// <summary>Defines the Initialize method that should be injected into the game loop.
        /// This method should initialize any long running game assets, systems, and services.
        /// </summary>
        Action Initialize { set; }
        
        /// <summary>Defines the LoadContent method that should be injected into the game loop.
        /// This method should load any game assets, images, sounds, models, etc.
        /// </summary>
        Action LoadContent { set; }

        /// <summary>Defines the Update method that should be injected into the game loop.
        /// This method should update the game state when the game loop ticks forward in time.
        /// </summary>
        Action<GameTime> Update { set; }

        /// <summary>Defines the Draw method that should be injected into the game loop.
        /// This method should render any assets, surfaces, textures, animations to a renderer.
        /// </summary>
        Action<GameTime> Draw { set; }

        /// <summary>Defines the UnloadContent method that should be injected into the game loop.
        /// This method should dispose of any game assets (especially any unmanaged resources).
        /// </summary>
        Action UnloadContent { set; }
        
        /// <summary>Used to create windows in which a renderer will display assets.
        /// </summary>
        IWindowFactory WindowFactory { get; }

        /// <summary>Used to create renderers tied to a window in which assets will be displayed.
        /// </summary>
        IRendererFactory RendererFactory { get; }

        /// <summary>Used to subscribe to game loop events.
        /// </summary>
        IEventManager EventManager { get; }

        /// <summary>Starts the game engine by initializing chosen subsystems.
        /// This method should start the game loop and utilize the various Action properties
        /// defined on the IGameEngine interface.
        /// </summary>
        /// <param name="initilizeTypes">Enumerated subsystems to initialize.</param>
        void Start(GameEngineInitializeType initilizeTypes);

        /// <summary>
        /// Ends the game engine. 
        /// This method should dispose of and stop any subsystems and remaining assets.
        /// </summary>
        void End();
    }
}