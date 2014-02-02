using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Graphics
{
	public class TrueTypeText : IDisposable
	{
		public string Text { get; private set; }

		public Font Font { get; private set; }

		public Color Color { get; private set; }

		public Texture Texture { get; private set; }

		public int OutlineSize { get { return Font.OutlineSize; } }

		public bool IsWrapped { get; set; }

		public int WrapLength { get; set; }

		public TrueTypeText(Renderer renderer, Surface surface, string text, Font textFont, Color color, int wrapLength)
			: this(renderer, surface, text, textFont, color, TextureAccessMode.Static, wrapLength)
		{
		}

		public TrueTypeText(Renderer renderer, Surface surface, string text, Font textFont, Color color, TextureAccessMode accessMode, int wrapLength)
		{
			Text = text;
			Font = textFont;
			Color = color;
			WrapLength = wrapLength;
			if (wrapLength > 0)
				IsWrapped = true;
			else
				IsWrapped = false;
			Texture = new Texture(renderer, surface, accessMode);
		}

		public void UpdateText(string text, int wrapLength = 0)
		{
			Text = text;

			Surface surface = new Surface(Font, Text, Color, wrapLength);
			Texture.UpdateSurfaceAndTexture(surface);
		}

		public void SetOutlineSize(int outlineSize)
		{
			Font.SetOutlineSize(outlineSize);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (Texture != null)
				Texture.Dispose();

			if (Font != null)
				Font.Dispose();
		}
	}
}
