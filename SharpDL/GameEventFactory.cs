using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public static class GameEventFactory<T>
		where T : GameEventArgs
	{
		public static T CreateGameEvent(SDL.SDL_Event rawEvent)
		{
			return Activator.CreateInstance(typeof(T),
				new object[] { rawEvent }) as T;
		}
	}
}
