using SDL2;

namespace SharpDL.Events
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
