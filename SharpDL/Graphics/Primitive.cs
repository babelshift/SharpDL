using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{
    public static class Primitive
    {
        public static void DrawLine(Renderer renderer, int x1, int y1, int x2, int y2)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException("renderer", Errors.E_RENDERER_NULL);
            }

            int result = SDL2.SDL.SDL_RenderDrawLine(renderer.Handle, x1, y1, x2, y2);

            if (Utilities.IsError(result))
            {
                throw new InvalidOperationException(Utilities.GetErrorMessage("SDL_RenderDrawLine: {0}"));
            }
        }
    }
}