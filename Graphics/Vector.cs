namespace SharpDL.Graphics
{
    public struct Vector
    {
        /// <summary>Horizontal component of the Vector.
        /// </summary>
        public float X { get; private set; }

        /// <summary>Vertical component of the Vector.
        /// </summary>
        public float Y { get; private set; }

        /// <summary>Vector with components (1, 1)
        /// </summary>
        public static Vector One => new Vector() { X = 1, Y = 1 };

        /// <summary>Vector with components (0, 0)
        /// </summary>
        public static Vector Zero => new Vector() { X = 0, Y = 0 };

        /// <summary>Constructs a Vector of size (x, y).
        /// </summary>
        /// <param name="x">Horizontal component of the Vector.</param>
        /// <param name="y">Vertical component of the Vector.</param>
        public Vector(float x, float y)
            : this()
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Transforms the Vector by the given Matrix.
        /// </summary>
        /// <param name="matrix">Matrix with which to perform transformation.</param>
        /// <returns>Transformed Vector.</returns>
        public Vector Transform(Matrix matrix)
        {
            return new Vector((X * matrix.Row1Col1) + (Y * matrix.Row2Col1), (X * matrix.Row2Col1) + (Y * matrix.Row2Col2));
        }

        /// <summary>Adds a Vector to this Vector.
        /// </summary>
        /// <param name="vector">Vector with which we perform addition.</param>
        /// <returns>New Vector as result of Vector addition.</returns>
        public Vector Add(Vector vector)
        {
            float x = X + vector.X;
            float y = Y + vector.Y;
            return new Vector(x, y);
        }

        /// <summary>Subtracts a Vector from this Vector.
        /// </summary>
        /// <param name="vector">Vector with which we perform subtraction.</param>
        /// <returns>New Vector as result of Vector subtraction.</returns>
        public Vector Subtract(Vector vector)
        {
            float x = X - vector.X;
            float y = Y - vector.Y;
            return new Vector(x, y);
        }

        /// <summary>Subtracts two Vectors.
        /// </summary>
        /// <param name="value1">Vector on left side of operator.</param>
        /// <param name="value2">Vector on right side of operator.</param>
        /// <returns>Result of Vector subtraction.</returns>
        public static Vector operator -(Vector value1, Vector value2)
        {
            return value1.Subtract(value2);
        }

        /// <summary>Adds two Vectors.
        /// </summary>
        /// <param name="value1">Vector on left side of operator.</param>
        /// <param name="value2">Vector on right side of operator.</param>
        /// <returns>Result of Vector addition.</returns>
        public static Vector operator +(Vector value1, Vector value2)
        {
            return value1.Add(value2);
        }

        /// <summary>Compares two Vectors for equality.
        /// </summary>
        /// <param name="value1">Vector on left side of operator.</param>
        /// <param name="value2">Vector on right side of operator.</param>
        /// <returns>True if Vectors match x, y. Otherwise false.</returns>
        public static bool operator ==(Vector value1, Vector value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }

        /// <summary>Compares two Vectors for non-equality.
        /// </summary>
        /// <param name="value1">Vector on left side of operator.</param>
        /// <param name="value2">Vector on right side of operator.</param>
        /// <returns>True if Vectors do not match x, y. Otherwise false.</returns>
        public static bool operator !=(Vector value1, Vector value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector)
            {
                var o = (Vector)obj;
                if (this.X == o.X && this.Y == o.Y)
                    return true;
                else
                    return false;
            }
            else
                return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}