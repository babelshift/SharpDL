using System;

namespace SharpDL.Graphics
{
    public struct Color
    {
        public byte R { get; private set; }

        public byte G { get; private set; }

        public byte B { get; private set; }

        public static Color Black => new Color(0, 0, 0);
        public static Color White => new Color(255, 255, 255);
        public static Color Red => new Color(255, 0, 0);
        public static Color Green => new Color(0, 255, 0);
        public static Color Blue => new Color(0, 0, 255);

        public Color(byte red, byte green, byte blue)
            : this()
        {
            R = red;
            G = green;
            B = blue;
        }

        public Color(string colorCode)
            : this()
        {
            string r = colorCode.Substring(0, 2);
            string g = colorCode.Substring(2, 2);
            string b = colorCode.Substring(4, 2);
            R = (byte)Convert.ToInt32(r, 16);
            G = (byte)Convert.ToInt32(g, 16);
            B = (byte)Convert.ToInt32(b, 16);
        }
    }
}