using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public struct Point
	{
		public int X { get; private set; }
		public int Y { get; private set; }

		public Point(int x, int y) : this()
		{
			X = x;
			Y = y;
		}
	}
}
