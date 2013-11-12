using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Input
{
	public enum MouseButtonCode : uint
	{
		Left = SDL.SDL_BUTTON_LEFT,
		Right = SDL.SDL_BUTTON_RIGHT,
		Middle = SDL.SDL_BUTTON_MIDDLE,
		X1 = SDL.SDL_BUTTON_X1,
		X2 = SDL.SDL_BUTTON_X2
	}
}
