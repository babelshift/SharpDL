using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Events
{
	public class GameEvent
	{
		//public bool IsAlreadyRaised { get; private set; }
		public GameEventArgs GameEventArgs { get; private set; }
		public EventHandler<GameEventArgs> GameEventHandler { get; private set; }

		public GameEvent(EventHandler<GameEventArgs> eventHandler, GameEventArgs eventArgs)
		{
			//IsAlreadyRaised = false;
			GameEventArgs = eventArgs;
			GameEventHandler = eventHandler;
		}

		public void RaiseEvent()
		{
			//if (!IsAlreadyRaised)
			//{
				if (GameEventHandler != null)
					GameEventHandler(this, GameEventArgs);
			//}
			//else
			//	IsAlreadyRaised = true;
		}
	}
}
