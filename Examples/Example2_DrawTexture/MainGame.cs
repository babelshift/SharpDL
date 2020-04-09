using System;
using SharpDL;
using SharpDL.Graphics;

namespace Example2_DrawTexture
{
    public class MainGame : Game
    {
        private Texture textureGitLogo;
        private Texture textureVisualStudioLogo;
        private Texture textureYboc;

        /// <summary>
        /// Initialize SDL2 and subsystems. Initialize Window and Renderer.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

			CreateWindow("Example 2 - Draw Texture", 100, 100, 1152, 720, WindowFlags.Shown);
			CreateRenderer(RendererFlags.RendererAccelerated | RendererFlags.RendererPresentVSync);
			Renderer.SetRenderLogicalSize(1152, 720);
        }

        /// <summary>
        /// Load external content such as images, audio, and tile maps
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent();

            // Creates an in memory SDL Surface from the PNG at the passed path
            Surface surfaceGitLogo = new Surface("Content/logo_git.png", SurfaceType.PNG);
            Surface surfaceVisualStudioLogo = new Surface("Content/logo_vs_2019.png", SurfaceType.PNG);
            Surface surfaceYboc = new Surface("Content/logo_yboc.png", SurfaceType.PNG);
            
            // Creates a GPU-driven SDL texture using the initialized renderer and created surface
            textureGitLogo = new Texture(Renderer, surfaceGitLogo);
            textureVisualStudioLogo = new Texture(Renderer, surfaceVisualStudioLogo);
            textureYboc = new Texture(Renderer, surfaceYboc);
        }
        
        /// <summary>
        /// Updates the drawn renderer after the update method has ticked the game forward.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            
            // Clear the screen on each iteration so that we don't get stale renders
            Renderer.ClearScreen();

            // Draw the Git logo at (0,0) and -45 degree angle rotated around the center (calculated Vector)
            textureGitLogo.Draw(0, 0, -45, new Vector(textureGitLogo.Width / 2, textureGitLogo.Height / 2));

            // Draw the Git logo at (300,300) and 45 degree angle rotated around the center (calculated Vector)
            textureGitLogo.Draw(300, 300, 45, new Vector(textureGitLogo.Width / 2, textureGitLogo.Height / 2));

            // Draw the Visual Studio logo at (700, 400) with no rotation
            textureVisualStudioLogo.Draw(700, 400);

            // Draw the YBOC logo at (900, 900) cropped to a 50x50 rectangle with (0,0) being the starting point
            textureYboc.Draw(800, 600, new Rectangle(0, 0, 50, 50)); 

            // Update the rendered state of the screen
            Renderer.RenderPresent();
        }
        
        /// <summary>
        /// Disposes of any content loaded in memory. Important for unmanaged resources like
        /// SDL2 textures. You'll get memory leaks if you don't dispose those!
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();

            textureGitLogo.Dispose();
            textureVisualStudioLogo.Dispose();
        }
    }
}