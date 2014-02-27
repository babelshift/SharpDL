using System.Linq;
using SDL2;
using System.Collections.Generic;
using System;
using SharpDL.Shared;

namespace SharpDL.Input
{
	public static class Mouse
	{
		public static int X { get; private set; }

		public static int Y { get; private set; }

		public static IEnumerable<MouseButtonCode> ButtonsPressed { get; private set; }

		public static IEnumerable<MouseButtonCode> PreviousButtonsPressed { get; private set; }

		/// <summary>
		/// Updates the mouse position to be used throughout the game update that takes place after mouse motion. You should
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public static void UpdateMousePosition(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static void UpdateMouseState()
		{
			PreviousButtonsPressed = ButtonsPressed;
			var currentMouseState = GetState();
			ButtonsPressed = currentMouseState.ButtonsPressed;
		}

		private static MouseState GetState()
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
			var buttonPressedMacroResult = SDL.SDL_BUTTON((uint)mouseButtonCode);
			var bitmaskComparisonResult = buttonsPressedBitmask & buttonPressedMacroResult;
			return bitmaskComparisonResult > 0;
		}

		public static void ShowCursor()
		{
			int result = SDL.SDL_ShowCursor(SDL.SDL_ENABLE);
			if (Utilities.IsError(result))
				throw new Exception(Utilities.GetErrorMessage("SDL_ShowCursor"));
		}

		public static void HideCursor()
		{
			int result = SDL.SDL_ShowCursor(SDL.SDL_DISABLE);
			if (Utilities.IsError(result))
				throw new Exception(Utilities.GetErrorMessage("SDL_HideCursor"));
		}
	}
}