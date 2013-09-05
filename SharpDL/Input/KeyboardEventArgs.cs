using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Input
{
	public class KeyboardEventArgs : GameEventArgs
	{
		private byte repeat;

		public enum KeyState
		{
			Pressed = SDL.SDL_PRESSED,
			Released = SDL.SDL_RELEASED
		}

		public KeyInformation KeyInformation { get; set; }
		public KeyState State { get; set; }
		public UInt32 WindowID { get; set; }

		public bool IsRepeat
		{
			get
			{
				if (repeat != 0) return true;
				else return false;
			}
		}

		public KeyboardEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			RawTimeStamp = rawEvent.key.timestamp;
			repeat = rawEvent.key.repeat;

			KeyInformation = new KeyInformation(rawEvent.key.keysym.scancode, rawEvent.key.keysym.sym, rawEvent.key.keysym.mod);
			State = (KeyState)rawEvent.key.state;
			WindowID = rawEvent.key.windowID;
		}
	}
}
