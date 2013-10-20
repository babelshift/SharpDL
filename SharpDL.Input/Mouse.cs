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

	public enum MouseButtonState
	{
		Pressed = SDL.SDL_PRESSED,
		Released = SDL.SDL_RELEASED
	}

	public struct MouseState
	{
		public IEnumerable<MouseButtonCode> ButtonsPressed;
		public int X;
		public int Y;
	}

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
	}
}
