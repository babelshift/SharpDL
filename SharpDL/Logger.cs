using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public static class Logger
	{
		public static void Info(String message)
		{
			SDL.SDL_Log(message, __arglist(String.Empty));
		}
	}
}
