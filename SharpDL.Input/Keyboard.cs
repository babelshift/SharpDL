using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace SharpDL.Input
{
	public static class Keyboard
    {
		public static KeyboardState GetKeyboardState()
		{
			int numKeys = 0;
			IntPtr keyboardStateArrayPointer = SDL2.SDL.SDL_GetKeyboardState(out numKeys);
			byte[] keyboardStateArray = new byte[numKeys];
			Marshal.Copy(keyboardStateArrayPointer, keyboardStateArray, 0, numKeys);

			List<KeyInformation> keyInformation = new List<KeyInformation>();
			foreach (PhysicalKeyCode physicalKeyCode in (PhysicalKeyCode[]) Enum.GetValues(typeof(PhysicalKeyCode)))
			{
				if (keyboardStateArray[(int)physicalKeyCode] == 1)
					keyInformation.Add(new KeyInformation((SDL2.SDL.SDL_Scancode)physicalKeyCode, SDL2.SDL.SDL_Keycode.SDLK_UNKNOWN, SDL2.SDL.SDL_Keymod.KMOD_NONE));
			}

			KeyboardState keyboardState = new KeyboardState();
			keyboardState.Keys = keyInformation;

			return keyboardState;
		}

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

