using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Input
{
	public class KeyInformation
	{
		public PhysicalKeyCode PhysicalKey { get; set; }
		public VirtualKeyCode VirtualKey { get; set; }
		public KeyModifier Modifier { get; set; }

		public KeyInformation(SDL.SDL_Scancode physicalKey, SDL.SDL_Keycode virtualKey, SDL.SDL_Keymod modifier)
		{
			PhysicalKey = (PhysicalKeyCode)physicalKey;
			VirtualKey = (VirtualKeyCode)virtualKey;
			Modifier = (KeyModifier)modifier;
		}
	}
}
