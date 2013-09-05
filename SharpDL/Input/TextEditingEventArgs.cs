using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Input
{
	public class TextEditingEventArgs : GameEventArgs
	{
		public int Length { get; set; }
		public int Start { get; set; }
		public String Text { get; set; }
		public UInt32 WindowID { get; set; }

		public TextEditingEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			RawTimeStamp = rawEvent.edit.timestamp;
		}
	}
}
