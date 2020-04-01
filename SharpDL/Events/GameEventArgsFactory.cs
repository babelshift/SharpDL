using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Events
{
	public static class GameEventArgsFactory<T>
		where T : class
	{
		public static T Create(SDL.SDL_Event rawEvent)
		{
			return Activator.CreateInstance(typeof(T),
				new object[] { rawEvent }) as T;
		}
	}
}
