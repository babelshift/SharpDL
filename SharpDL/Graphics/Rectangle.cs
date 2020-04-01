using System;

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

        public Point Location { get { return new Point(X, Y); } }

        public Point Center
        {
            get
            {
                return new Point(this.X + (this.Width / 2), this.Y + (this.Height / 2));
            }
        }

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
            if (Left <= point.X && Right >= point.X && Top <= point.Y && Bottom >= point.Y)
                return true;
            else
                return false;
        }

        public bool Contains(Rectangle rectangle)
        {
            if (Left <= rectangle.Left && Right >= rectangle.Right && Top <= rectangle.Top && Bottom >= rectangle.Bottom)
                return true;
            else
                return false;
        }

        public bool Contains(Vector vector)
        {
            if (Left <= vector.X && Right >= vector.X && Top <= vector.Y && Bottom >= vector.Y)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Determines if two rectangles intersect.
        /// </summary>
        /// <param name="rectangle">Rectangle.</param>
        public bool Intersects(Rectangle rectangle)
        {
            return rectangle.Left <= Right && Left <= rectangle.Right && rectangle.Top <= Bottom && Top <= rectangle.Bottom;
        }

        public Vector GetIntersectionDepth(Rectangle rectangle)
        {
            // Calculate half sizes.
            float halfWidthA = this.Width / 2.0f;
            float halfHeightA = this.Height / 2.0f;
            float halfWidthB = rectangle.Width / 2.0f;
            float halfHeightB = rectangle.Height / 2.0f;

            // Calculate centers.
            Vector centerA = new Vector(this.Left + halfWidthA, this.Top + halfHeightA);
            Vector centerB = new Vector(rectangle.Left + halfWidthB, rectangle.Top + halfHeightB);

            // Calculate current and minimum-non-intersecting distances between centers.
            float distanceX = centerA.X - centerB.X;
            float distanceY = centerA.Y - centerB.Y;
            float minDistanceX = halfWidthA + halfWidthB;
            float minDistanceY = halfHeightA + halfHeightB;

            // If we are not intersecting at all, return (0, 0).
            if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
                return Vector.Zero;

            // Calculate and return intersection depths.
            float depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
            float depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
            return new Vector(depthX, depthY);
        }
    }
}