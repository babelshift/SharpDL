using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Events
{
	public class TextInputEventArgs : GameEventArgs
	{
		public String Text { get; set; }

		public UInt32 WindowID { get; set; }

		public TextInputEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			RawTimeStamp = rawEvent.text.timestamp;
			WindowID = rawEvent.text.windowID;

			byte[] rawBytes = new byte[SDL2.SDL.SDL_TEXTINPUTEVENT_TEXT_SIZE];

			// we have an unsafe pointer to a char array from SDL, explicitly marshal this to a byte array of fixed size
			unsafe { Marshal.Copy((IntPtr)rawEvent.text.text, rawBytes, 0, SDL.SDL_TEXTINPUTEVENT_TEXT_SIZE); }

			// look for the index of the first null terminator
			int length = Array.IndexOf(rawBytes, (byte)0);

			// according to the SDL2 migration guide, TextInputEvent will have a UTF-8 encoded string
			Text = Encoding.UTF8.GetString(rawBytes, 0, length);
		}
	}
}
