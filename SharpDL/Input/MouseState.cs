using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Input
{
	public struct MouseState
	{
		public IEnumerable<MouseButtonCode> ButtonsPressed;
		public int X;
		public int Y;
	}
}
