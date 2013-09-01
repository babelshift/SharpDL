using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public class MouseWheelEventArgs : GameEventArgs
	{
		public UInt32 WindowID { get; private set; }
		public UInt32 MouseDeviceID { get; private set; }
		public int HorizontalScrollAmount { get; private set; }
		public int VerticalScrollAmount { get; private set; }

		public MouseWheelEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			RawTimeStamp = rawEvent.wheel.timestamp;
			WindowID = rawEvent.wheel.windowID;
			MouseDeviceID = rawEvent.wheel.which;
			HorizontalScrollAmount = rawEvent.wheel.x;
			VerticalScrollAmount = rawEvent.wheel.y;
		}
	}
}
