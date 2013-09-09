using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Input
{
	public struct MouseState
	{
		public int X;
		public int Y;
	}

	public static class Mouse
	{
		public static MouseState GetState()
		{
			int x = 0;
			int y = 0;
			SDL.SDL_GetMouseState(out x, out y);
			return new MouseState() { X = x, Y = y };
		}
	}
}
