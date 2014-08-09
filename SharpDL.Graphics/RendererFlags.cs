using SDL2;
using System;

namespace SharpDL.Graphics
{
    [Flags]
    public enum RendererFlags
    {
        RendererAccelerated = SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED,
        RendererPresentVSync = SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC,
        SupportRenderTargets = SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE
    }
}