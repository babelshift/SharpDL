using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public class TrueTypeText : Texture, IDisposable
	{
		public string Text { get; private set; }
		public Font Font { get; private set; }
		public Color Color { get; private set; }

		public TrueTypeText(Renderer renderer, Surface surface, string text, Font textFont, Color color)
			: base(renderer, surface, TextureAccessMode.Static)
		{
			Text = text;
			Font = textFont;
			Color = color;
		}

		public TrueTypeText(Renderer renderer, Surface surface, string text, Font textFont, Color color, TextureAccessMode accessMode)
			: base(renderer, surface, accessMode)
		{
			Text = text;
			Font = textFont;
			Color = color;
		}

		public void UpdateText(string text)
		{
			Text = text;

			Surface surface = new Surface(Font, Text, Color);
			UpdateSurfaceAndTexture(surface);
		}

		public override void Dispose()
		{
			base.Dispose();

			Font.Dispose();
		}
	}
}
