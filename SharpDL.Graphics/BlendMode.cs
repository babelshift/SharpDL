using SDL2;
using System;

namespace SharpDL.Graphics
{
    [Flags]
    public enum BlendMode
    {
        None = SDL.SDL_BlendMode.SDL_BLENDMODE_NONE,
        Blend = SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND,
        Add = SDL.SDL_BlendMode.SDL_BLENDMODE_ADD,
        Mod = SDL.SDL_BlendMode.SDL_BLENDMODE_MOD,
    }
}