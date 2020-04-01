using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Events
{
	public enum WindowEventType
	{
		Shown = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SHOWN,
		Hidden = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_HIDDEN,
		Exposed = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED,
		Moved = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MOVED,
		Resized = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED,
		SizeChanged = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED,
		Minimized = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MINIMIZED,
		Maximized = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_MAXIMIZED,
		Restored = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESTORED,
		Enter = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_ENTER,
		Leave = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_LEAVE,
		FocusGained = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED,
		FocusLost = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST,
		Close = SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE
	}
}
