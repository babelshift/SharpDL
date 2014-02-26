using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Shared
{
    public static class Utilities
    {
		public static bool IsError(int errorCode)
		{
			if (errorCode < 0)
				return true;
			return false;
		}

		public static string GetErrorMessage(string method)
		{
			return String.Format("{0}: {1}", method, SDL.SDL_GetError());
		}
    }
}
