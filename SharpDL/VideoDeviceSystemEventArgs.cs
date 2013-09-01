using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public class VideoDeviceSystemEventArgs : GameEventArgs
	{
		public VideoDeviceSystemEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			RawTimeStamp = rawEvent.syswm.timestamp;
		}
	}
}
