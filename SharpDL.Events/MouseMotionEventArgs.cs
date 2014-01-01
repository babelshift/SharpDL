using SDL2;
using SharpDL.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Events
{
	public class MouseMotionEventArgs : GameEventArgs
	{
		public UInt32 WindowID { get; private set; }

		public UInt32 MouseDeviceID { get; private set; }

		public IList<MouseButtonCode> MouseButtonsPressed { get; private set; }

		public int RelativeToWindowX { get; private set; }

		public int RelativeToWindowY { get; private set; }

		public int RelativeToLastMotionEventX { get; private set; }

		public int RelativeToLastMotionEventY { get; private set; }

		public MouseMotionEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			RawTimeStamp = rawEvent.motion.timestamp;
			WindowID = rawEvent.motion.windowID;
			MouseDeviceID = rawEvent.motion.which;
			RelativeToWindowX = rawEvent.motion.x;
			RelativeToWindowY = rawEvent.motion.y;
			RelativeToLastMotionEventX = rawEvent.motion.xrel;
			RelativeToLastMotionEventY = rawEvent.motion.yrel;

			List<MouseButtonCode> buttonsPressed = new List<MouseButtonCode>();
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_LEFT)
				buttonsPressed.Add(MouseButtonCode.Left);
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_MIDDLE)
				buttonsPressed.Add(MouseButtonCode.Middle);
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_RIGHT)
				buttonsPressed.Add(MouseButtonCode.Right);
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_X1)
				buttonsPressed.Add(MouseButtonCode.X1);
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_X2)
				buttonsPressed.Add(MouseButtonCode.X2);
			MouseButtonsPressed = buttonsPressed;
		}
	}
}
