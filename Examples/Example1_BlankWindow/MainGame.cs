using SharpDL;
using SharpDL.Graphics;

namespace Example1_BlankWindow
{
    public class MainGame : Game
    {
		/// <summary>
		/// Initialize SDL and any sub-systems. Window and Renderer must be initialized before use.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();

			CreateWindow("Example 1 - Blank Window", 100, 100, 1152, 720, WindowFlags.Shown);
			CreateRenderer(RendererFlags.RendererAccelerated | RendererFlags.RendererPresentVSync);
			Renderer.SetRenderLogicalSize(1152, 720);
		}

		/// <summary>
		/// Load any game assets such as textures and audio.
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();
		}

		/// <summary>
		/// Update the state of the game.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		/// <summary>
		/// Render the current state of the game.
		/// </summary>
		/// <param name="gameTime"></param>
		protected override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);
			Renderer.RenderPresent();
		}

		/// <summary>
		/// Unload and dispose of any assets. Remember to dispose SDL-native objects!
		/// </summary>
		protected override void UnloadContent()
		{
			base.UnloadContent();
		}
	}
}