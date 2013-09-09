using SDL2;
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
		public IList<MouseButtonEventArgs.MouseButtonCode> MouseButtonsPressed { get; private set; }
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

			List<MouseButtonEventArgs.MouseButtonCode> buttonsPressed = new List<MouseButtonEventArgs.MouseButtonCode>();
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_LEFT)
				buttonsPressed.Add(MouseButtonEventArgs.MouseButtonCode.Left);
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_MIDDLE)
				buttonsPressed.Add(MouseButtonEventArgs.MouseButtonCode.Middle);
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_RIGHT)
				buttonsPressed.Add(MouseButtonEventArgs.MouseButtonCode.Right);
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_X1)
				buttonsPressed.Add(MouseButtonEventArgs.MouseButtonCode.X1);
			if (SDL.SDL_BUTTON(rawEvent.motion.state) == SDL.SDL_BUTTON_X2)
				buttonsPressed.Add(MouseButtonEventArgs.MouseButtonCode.X2);
			MouseButtonsPressed = buttonsPressed;
		}
	}
}
