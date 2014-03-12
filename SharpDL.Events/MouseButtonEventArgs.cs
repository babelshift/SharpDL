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

		/// <summary>
		/// Mouse X position relative to the window origin. Setter is public because we may need to intercept and adjust the value for detecting click/hover events
		/// for controls rendered within render targets. X position could be different relative to window origin when compared to relative to render target position.
		/// </summary>
		public int RelativeToWindowX { get; set; }

		/// <summary>
		/// Mouse Y position relative to the window origin. Setter is public because we may need to intercept and adjust the value for detecting click/hover events
		/// for controls rendered within render targets. X position could be different relative to window origin when compared to relative to render target position.
		/// </summary>
		public int RelativeToWindowY { get; set; }

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
