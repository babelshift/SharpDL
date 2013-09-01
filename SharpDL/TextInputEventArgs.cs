using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public class TextInputEventArgs : GameEventArgs
	{
		public String Text { get; set; }
		public UInt32 WindowID { get; set; }

		public TextInputEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			RawTimeStamp = rawEvent.text.timestamp;
		}
	}
}
