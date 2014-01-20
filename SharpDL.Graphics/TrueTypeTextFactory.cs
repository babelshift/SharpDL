using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public static class TrueTypeTextFactory
	{
		public static TrueTypeText CreateTrueTypeText(Renderer renderer, string fontPath, int fontSize, Color color, string text, int wrapLength)
		{
			Font font = new Font(fontPath, fontSize);
			Surface surface = new Surface(font, text, color, wrapLength);
			TrueTypeText trueTypeText = new TrueTypeText(renderer, surface, text, font, color, wrapLength);
			return trueTypeText;
		}
	}
}
