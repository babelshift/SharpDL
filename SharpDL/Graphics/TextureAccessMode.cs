using SDL2;

namespace SharpDL.Graphics
{
    public enum TextureAccessMode
    {
        Static = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STATIC,
        Streaming = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_STREAMING,
        Target = SDL.SDL_TextureAccess.SDL_TEXTUREACCESS_TARGET
    }
}