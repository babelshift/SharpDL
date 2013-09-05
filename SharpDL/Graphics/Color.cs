using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public struct Color
	{
		public byte R { get; private set; }
		public byte G { get; private set; }
		public byte B { get; private set; }
		public byte A { get; private set; }

		public Color(byte r, byte g, byte b, byte a) : this()
		{
			R = r;
			G = g;
			B = b;
			A = a;
		}
	}
}
