using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public static class TrueTypeTextFactory
	{
		public static TrueTypeText CreateTrueTypeText(Renderer renderer, string fontPath, int fontSize, Color color)
		{
			Font font = new Font(fontPath, fontSize);
			Surface surface = new Surface(font, ".", color);
			TrueTypeText text = new TrueTypeText(renderer, surface, ".", font, color);
			return text;
		}
	}
}
