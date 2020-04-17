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

        private ITexture textureGitLogo;
        private ITexture textureVisualStudioLogo;
        private ITexture textureYboc;

		private ITrueTypeText ttf;
		
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
            // Creates an in memory SDL Surface from the PNG at the passed path
            ISurface surfaceGitLogo = engine.SurfaceFactory.CreateSurface("Content/logo_git.png", SurfaceType.PNG);
            ISurface surfaceVisualStudioLogo = engine.SurfaceFactory.CreateSurface("Content/logo_vs_2019.png", SurfaceType.PNG);
            ISurface surfaceYboc = engine.SurfaceFactory.CreateSurface("Content/logo_yboc.png", SurfaceType.PNG);
            
            // Creates a GPU-driven SDL texture using the initialized renderer and created surface
            textureGitLogo = engine.TextureFactory.CreateTexture(renderer, surfaceGitLogo);
            textureVisualStudioLogo = engine.TextureFactory.CreateTexture(renderer, surfaceVisualStudioLogo);
            textureYboc = engine.TextureFactory.CreateTexture(renderer, surfaceYboc);

			ttf = engine.TrueTypeTextFactory.CreateTrueTypeText(renderer, "Content/Adumu.ttf", "Hello world!", 24, Color.White);
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
            // Clear the screen on each iteration so that we don't get stale renders
            renderer.ClearScreen();

            // Draw the Git logo at (0,0) and -45 degree angle rotated around the center (calculated Vector)
            textureGitLogo.Draw(0, 0, -45, new Vector(textureGitLogo.Width / 2, textureGitLogo.Height / 2));

            // Draw the Git logo at (300,300) and 45 degree angle rotated around the center (calculated Vector)
            textureGitLogo.Draw(300, 300, 45, new Vector(textureGitLogo.Width / 2, textureGitLogo.Height / 2));

            // Draw the Visual Studio logo at (700, 400) with no rotation
            textureVisualStudioLogo.Draw(700, 400);

            // Draw the YBOC logo at (900, 900) cropped to a 50x50 rectangle with (0,0) being the starting point
            textureYboc.Draw(800, 600, new Rectangle(0, 0, 50, 50)); 

			// Draw text at 400,100
			ttf.Texture.Draw(400, 100);

            // Update the rendered state of the screen
			renderer.RenderPresent();
		}

		/// <summary>Unload and dispose of any assets. Remember to dispose SDL-native objects!
		/// </summary>
		private void UnloadContent()
		{
            textureGitLogo.Dispose();
            textureVisualStudioLogo.Dispose();
		}
	}
}