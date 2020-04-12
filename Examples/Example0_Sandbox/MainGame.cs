using Microsoft.Extensions.Logging;
using SharpDL;
using SharpDL.Graphics;

namespace Example0_Sandbox
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
			engine.LoadContent = () => LoadContent();
			engine.Update = (gameTime) => Update(gameTime);
			engine.Draw = (gameTime) => Draw(gameTime);
			engine.UnloadContent = () => UnloadContent();
        }

		public void Run()
		{
			engine.Start(GameEngineInitializeType.Everything);
		}

		/// <summary>Initialize SDL and any sub-systems. Window and Renderer must be initialized before use.
		/// </summary>
		private void Initialize()
		{
			window = engine.WindowFactory.CreateWindow("Example 0 - Sandbox");
			renderer = engine.RendererFactory.CreateRenderer(window);
			renderer.SetRenderLogicalSize(1152, 720);
		}

		/// <summary>Load any game assets such as textures and audio.
		/// </summary>
		private void LoadContent()
		{
		}

		/// <summary>Update the state of the game.
		/// </summary>
		/// <param name="gameTime"></param>
		private void Update(GameTime gameTime)
		{
		}

		/// <summary>Render the current state of the game.
		/// </summary>
		/// <param name="gameTime"></param>
		private void Draw(GameTime gameTime)
		{
			renderer.RenderPresent();
		}

		/// <summary>Unload and dispose of any assets. Remember to dispose SDL-native objects!
		/// </summary>
		private void UnloadContent()
		{
		}
	}
}