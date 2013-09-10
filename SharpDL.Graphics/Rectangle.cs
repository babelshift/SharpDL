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

		public static Rectangle Empty
		{
			get
			{
				return new Rectangle()
				{
					Height = 0,
					Width = 0,
					X = 0,
					Y = 0
				};
			}
		}

		public bool IsEmpty
		{
			get
			{
				if (Width == 0 && Height == 0)
					return true;
				else
					return false;
			}
		}

		public Point Location { get { return new Point((float)X, (float)Y); } }

		public Rectangle(int x, int y, int width, int height)
			: this()
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public bool Contains(Point point)
		{
			if (point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom)
				return true;
			else
				return false;
		}

		public bool Contains(Rectangle rectangle)
		{
			if (rectangle.Left >= Left && rectangle.Right <= Right && rectangle.Top >= Top && rectangle.Bottom <= Bottom)
				return true;
			else
				return false;
		}

		public bool Intersects(Rectangle rectangle)
		{
			Point topLeft = new Point(rectangle.X, rectangle.Y);
			Point bottomLeft = new Point(rectangle.X, rectangle.Bottom);
			Point topRight = new Point(rectangle.Y, rectangle.Right);
			Point bottomRight = new Point(rectangle.Right, rectangle.Bottom);

			if(this.Contains(topLeft) || this.Contains(bottomLeft) || this.Contains(topRight) || this.Contains(bottomRight))
				return true;
			else
				return false;
		}
	}
}
