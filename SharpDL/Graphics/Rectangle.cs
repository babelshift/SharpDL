using System;

namespace SharpDL.Graphics
{
    public struct Rectangle
    {
        /// <summary>Top left corner X coordinate.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Top left corner Y coordinate.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Width of the Rectangle in pixels.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Height of the Rectangle in pixels.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Equal to Y coordinate plus the Height.
        /// </summary>
        public int Bottom => Y + Height;

        /// <summary>Equal to Y.
        /// </summary>
        public int Top => Y;

        /// <summary>Equal to X.
        /// </summary>
        public int Left => X;

        /// <summary>Equal to X plus the Width.
        /// </summary>
        public int Right => X + Width;

        /// <summary>Returns a Rectangle at (0,0) with 0 width and 0 height.
        /// </summary>
        /// <returns></returns>
        public static Rectangle Empty => new Rectangle(0, 0, 0, 0);

        /// <summary>
        /// Returns true if width = 0 and height = 0.
        /// </summary>
        public bool IsEmpty => Width == 0 && Height == 0;

        /// <summary>Returns a Point in 2D space at the top left corner of the Rectangle.
        /// </summary>
        public Point Location => new Point(X, Y);

        /// <summary>Returns a 2D point closest to the center of the Rectangle.
        /// </summary>
        public Point Center => new Point(this.X + (this.Width / 2), this.Y + (this.Height / 2));

        /// <summary>
        /// Constructs a new rectangle.
        /// </summary>
        /// <param name="x">X coordinate of top left corner</param>
        /// <param name="y">Y coordinate of top left corner.</param>
        /// <param name="width">Width in pixels</param>
        /// <param name="height">Height in pixels</param>
        public Rectangle(int x, int y, int width, int height)
            : this()
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than or equal to 0.");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Height must be greater than or equal to 0.");
            }

            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Indicates if a Point falls within the bounds of the Rectangle.
        /// </summary>
        /// <param name="point">Point to check within a Rectangle.</param>
        /// <returns>True if the Point is contained within the Rectangle. Otherwise false.</returns>
        public bool Contains(Point point)
        {
            return (Left <= point.X) && (Right >= point.X) && (Top <= point.Y) && (Bottom >= point.Y);
        }

        /// <summary>
        /// Indicates if a Rectangle is fully contained within this Rectangle.
        /// </summary>
        /// <param name="rectangle">Rectangle to check if fully contained within this instanced Rectangle.</param>
        /// <returns>True if the Rectangle is contained within this instanced Rectangle. Otherwise false.</returns>
        public bool Contains(Rectangle rectangle)
        {
            return (Left <= rectangle.Left) && (Right >= rectangle.Right) && (Top <= rectangle.Top) && (Bottom >= rectangle.Bottom);
        }

        /// <summary>
        /// Indicates if a Vector is fully contained within this Rectangle.
        /// </summary>
        /// <param name="vector">Vector to check if fully contained within this instanced Rectangle.</param>
        /// <returns>True if the Vector is contained within this instanced Rectangle. Otherwise false.</returns>
        public bool Contains(Vector vector)
        {
            return (Left <= vector.X) && (Right >= vector.X) && (Top <= vector.Y) && (Bottom >= vector.Y);
        }


        /// <summary>Determines if two Rectangles intersect.
        /// </summary>
        /// <param name="rectangle">Rectangle to check if intersects with this instanced Rectangle.</param>
        /// <returns>True if the Rectangles intersect. Otherwise false.
        public bool Intersects(Rectangle rectangle)
        {
            return (rectangle.Left <= Right) && (Left <= rectangle.Right) && (rectangle.Top <= Bottom) && (Top <= rectangle.Bottom);
        }

        /// <summary>
        /// Calculates the depth at which two Rectangles are intersecting.
        /// </summary>
        /// <param name="rectangle">Rectangle aginst which to check the intersection depth.</param>
        /// <returns>Vector containing the depth at which the Rectangles intersect.</returns>
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