using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public struct Vector
	{
		public float X { get; private set; }
		public float Y { get; private set; }

		public static Vector One { get { return new Vector() { X = 1, Y = 1 }; } }
		public static Vector Zero { get { return new Vector() { X = 0, Y = 0 }; } }

		public Vector(float x, float y) : this()
		{
			X = x;
			Y = y;
		}

		public Vector Add(Vector vector)
		{
			float x = X + vector.X;
			float y = Y + vector.Y;
			return new Vector(x, y);
		}

		public static Vector operator +(Vector value1, Vector value2)
		{
			return value1.Add(value2);
		}

		public static bool operator ==(Vector value1, Vector value2)
		{
			return value1.X == value2.X || value1.Y == value2.Y;
		}

		public static bool operator !=(Vector value1, Vector value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y;
		}
	}
}
