using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public struct Rectangle
	{
		public int X { get; private set; }
		public int Y { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public int Bottom { get { return Y + Height; } }
		public int Top { get { return Y; } }
		public int Left { get { return X; } }
		public int Right { get { return X + Width; } }

		public Rectangle(int x, int y, int width, int height)
			: this()
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}
	}
}
