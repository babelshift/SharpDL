using System;
using SDL2;

namespace SharpDL
{
    [Flags]
    public enum InitializeType : uint
    {
        Timer = SDL.SDL_INIT_TIMER,
        Audio = SDL.SDL_INIT_AUDIO,
        Video = SDL.SDL_INIT_VIDEO,
        Joystick = SDL.SDL_INIT_JOYSTICK,
        Haptic = SDL.SDL_INIT_HAPTIC,
        GameController = SDL.SDL_INIT_GAMECONTROLLER,
        Events = SDL.SDL_INIT_EVENTS,
        Sensor = SDL.SDL_INIT_SENSOR,
        Everything = SDL.SDL_INIT_EVERYTHING,
        NoParachute = SDL.SDL_INIT_NOPARACHUTE
    }
}