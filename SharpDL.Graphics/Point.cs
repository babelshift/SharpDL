using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public struct Point
	{
		public float X { get; private set; }
		public float Y { get; private set; }

		public Point(float x, float y) : this()
		{
			X = x;
			Y = y;
		}
	}
}
