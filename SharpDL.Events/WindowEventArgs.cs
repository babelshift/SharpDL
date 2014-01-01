using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Events
{
	public class WindowEventArgs : GameEventArgs
	{
		public int Data1 { get; private set; }

		public int Data2 { get; private set; }

		public WindowEventType SubEventType { get; private set; }

		public UInt32 WindowID { get; private set; }

		public WindowEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			SubEventType = (WindowEventType)rawEvent.window.windowEvent;
			Data1 = rawEvent.window.data1;
			Data2 = rawEvent.window.data2;
			RawTimeStamp = rawEvent.window.timestamp;
			WindowID = rawEvent.window.windowID;
		}
	}
}
