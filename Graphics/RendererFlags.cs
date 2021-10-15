using SDL2;
using System;

namespace SharpDL.Graphics
{
    [Flags]
    public enum RendererFlags : uint
    {
        None = 0,
        RendererAccelerated = SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED,
        RendererPresentVSync = SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC,
        SupportRenderTargets = SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE
    }
}