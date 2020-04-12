using Microsoft.Extensions.Logging;
using SharpDL;
using SharpDL.Graphics;

namespace Example3_EventHandling
{
    public class MainGame : IGame
    {
        private readonly ILogger<MainGame> logger;
        private IGameEngine engine;
        private IWindow window;
        private IRenderer renderer;

        public MainGame(
            IGameEngine engine,
            ILogger<MainGame> logger = null)
        {
            this.engine = engine;
            this.logger = logger;
            engine.Initialize = () => Initialize();
            engine.LoadContent = () => {};
            engine.Update = (gameTime) => {};
            engine.Draw = (gameTime) => {};
            engine.UnloadContent = () => {};
        }

        public void Run()
        {
            engine.Start(GameEngineInitializeType.Everything);
        }

        /// <summary>Initialize SDL and any sub-systems. Window and Renderer must be initialized before use.
        /// </summary>
        private void Initialize()
        {
            window = engine.WindowFactory.CreateWindow("Example 3 - Event Handling");
            renderer = engine.RendererFactory.CreateRenderer(window);
            renderer.SetRenderLogicalSize(1152, 720);

            engine.EventManager.KeyReleased += (sender, eventArgs) => 
            {
                logger.LogTrace($"Key released event: State = {eventArgs.State}, VirtualKey = {eventArgs.KeyInformation.VirtualKey}, PhysicalKey = {eventArgs.KeyInformation.PhysicalKey}.");
            };
        }
    }
}