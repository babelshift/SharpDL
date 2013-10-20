using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using SharpDL.Input;

namespace SharpDL.Events
{
	public class MouseButtonEventArgs : GameEventArgs
	{
		public UInt32 WindowID { get; private set; }
		public UInt32 MouseDeviceID { get; private set; }
		public MouseButtonCode MouseButton { get; private set; }
		public MouseButtonState State { get; private set; }
		public int RelativeToWindowX { get; private set; }
		public int RelativeToWindowY { get; private set; }

		public MouseButtonEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			RawTimeStamp = rawEvent.button.timestamp;
			WindowID = rawEvent.button.windowID;
			MouseDeviceID = rawEvent.button.which;
			MouseButton = (MouseButtonCode)rawEvent.button.button;
			State = (MouseButtonState)rawEvent.button.state;
			RelativeToWindowX = rawEvent.button.x;
			RelativeToWindowY = rawEvent.button.y;
		}
	}
}
