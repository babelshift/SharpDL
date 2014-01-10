using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace SharpDL.Input
{
	public static class Keyboard
    {
		public static void StartTextInput()
		{
			SDL2.SDL.SDL_StartTextInput();
		}

		public static void StopTextInput()
		{
			SDL2.SDL.SDL_StopTextInput();
		}
    }
}

