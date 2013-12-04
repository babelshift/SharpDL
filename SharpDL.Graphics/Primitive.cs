using System;

namespace SharpDL.Graphics
{
	public static class Primitive
    {
		public static void DrawLine(Renderer renderer, int x1, int y1, int x2, int y2)
		{
			int result = SDL2.SDL.SDL_RenderDrawLine(renderer.Handle, x1, y1, x2, y2);

			if(result < 0)
				throw new Exception(String.Format("SDL_RenderDrawLine: {0}", SDL2.SDL.SDL_GetError()));
		}
    }
}

