using SDL2;
using SharpDL.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public static class MessageBox
	{
		public enum MessageBoxType : uint
		{
			Error = SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR,
			Information = SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION,
			Warning = SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_WARNING
		}

		public static void Show(MessageBoxType messageBoxType, 
			String title, String message, Window parentWindow = null)
		{
			IntPtr parentWindowHandle = IntPtr.Zero;
			if (parentWindow != null)
				parentWindowHandle = parentWindow.Handle;

			SDL.SDL_ShowSimpleMessageBox((SDL.SDL_MessageBoxFlags)messageBoxType,
				title, message, parentWindowHandle);
		}
	}
}
