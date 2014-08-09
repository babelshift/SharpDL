namespace SharpDL.Graphics
{
    public struct Point
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Point(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Point value1, Point value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }

        public static bool operator !=(Point value1, Point value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point)
            {
                var o = (Point)obj;
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