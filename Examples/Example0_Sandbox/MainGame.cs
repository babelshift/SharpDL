using Microsoft.Extensions.Logging;
using SharpDL;
using SharpDL.Graphics;

namespace Example0_Sandbox
{
    public class MainGame : Game
    {
		private readonly ILogger<MainGame> logger;

		public MainGame(
			IWindowFactory windowFactory,
			IRendererFactory rendererFactory,
			ILogger<MainGame> logger = null, 
			ILogger<Game> baseLogger = null) 
			: base(windowFactory, rendererFactory, baseLogger)
		{
			this.logger = logger;
        }

		/// <summary>Initialize SDL and any sub-systems. Window and Renderer must be initialized before use.
		/// </summary>
		protected override void Initialize()
		{
			Window = WindowFactory.CreateWindow("Example 0 - Sandbox");
			Renderer = RendererFactory.CreateRenderer(Window);
			Renderer.SetRenderLogicalSize(1152, 720);
		}

		/// <summary>Load any game assets such as textures and audio.
		/// </summary>
		protected override void LoadContent()
		{
		}

		/// <summary>Update the state of the game.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Update(GameTime gameTime)
		{
		}

		/// <summary>Render the current state of the game.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Draw(GameTime gameTime)
		{
			Renderer.RenderPresent();
		}

		/// <summary>Unload and dispose of any assets. Remember to dispose SDL-native objects!
		/// </summary>
		protected override void UnloadContent()
		{
		}
	}
}