using SDL2;
using System;

namespace SharpDL.Shared
{
    public static class Utilities
    {
		public static bool IsError(int errorCode)
		{
            if (errorCode < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
		}

		public static string GetErrorMessage(string method)
		{
			return String.Format("{0}: {1}", method, SDL.SDL_GetError());
		}

        public static bool IsNull(object argument)
        {
            if (argument == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
