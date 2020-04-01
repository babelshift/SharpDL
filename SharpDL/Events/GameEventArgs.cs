using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Events
{
	public abstract class GameEventArgs : EventArgs
	{
		protected SDL.SDL_Event RawEvent { get; private set; }

		protected uint RawTimeStamp { get; set; }

		public GameEventType EventType { get; private set; }

		public TimeSpan TimeStamp { get { return new TimeSpan(RawTimeStamp); } }

		public GameEventArgs(SDL.SDL_Event rawEvent)
		{
			RawEvent = rawEvent;
			EventType = (GameEventType)rawEvent.type;
		}
	}
}
