using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Input
{
	public static class Mouse
	{
		public static MouseState GetState()
		{
			int x = 0;
			int y = 0;
			uint buttonBitMask = SDL.SDL_GetMouseState(out x, out y);

			List<MouseButtonCode> buttonsPressed = new List<MouseButtonCode>();

			if (IsButtonPressed(buttonBitMask, MouseButtonCode.Left))
				buttonsPressed.Add(MouseButtonCode.Left);
			if (IsButtonPressed(buttonBitMask, MouseButtonCode.Right))
				buttonsPressed.Add(MouseButtonCode.Right);
			if (IsButtonPressed(buttonBitMask, MouseButtonCode.Middle))
				buttonsPressed.Add(MouseButtonCode.Middle);
			if (IsButtonPressed(buttonBitMask, MouseButtonCode.X1))
				buttonsPressed.Add(MouseButtonCode.X1);
			if (IsButtonPressed(buttonBitMask, MouseButtonCode.X2))
				buttonsPressed.Add(MouseButtonCode.X2);

			return new MouseState() { X = x, Y = y, ButtonsPressed = buttonsPressed };
		}

		private static bool IsButtonPressed(uint buttonsPressedBitmask, MouseButtonCode mouseButtonCode)
		{
			if ((buttonsPressedBitmask & (uint)mouseButtonCode) == 1)
				return true;
			else
				return false;
		}

		public static void ShowCursor()
		{
			SDL2.SDL.SDL_ShowCursor(SDL2.SDL.SDL_ENABLE);
		}

		public static void HideCursor()
		{
			SDL2.SDL.SDL_ShowCursor(SDL2.SDL.SDL_DISABLE);
		}
	}
}
