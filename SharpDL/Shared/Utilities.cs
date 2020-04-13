using SDL2;

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
			return $"{method}: {SDL.SDL_GetError()}";
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
