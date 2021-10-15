using SDL2;

namespace SharpDL.Events
{
	public class QuitEventArgs : GameEventArgs
	{
		public QuitEventArgs(SDL.SDL_Event rawEvent)
			: base(rawEvent)
		{
			RawTimeStamp = rawEvent.quit.timestamp;
		}
	}
}
