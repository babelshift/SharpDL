using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{
    public class TrueTypeText : ITrueTypeText
    {
        public string Text { get; private set; }

        public IFont Font { get; private set; }

        public Color Color { get; private set; }

        public ITexture Texture { get; private set; }

        public int OutlineSize { get { return Font.OutlineSize; } }

        public bool IsWrapped { get; set; }

        public int WrapLength { get; set; }

        public TrueTypeText(IRenderer renderer, ISurface surface, string text, Font font, Color color, int wrapLength)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            if (surface == null)
            {
                throw new ArgumentNullException(nameof(surface));
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            if (font == null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            if (wrapLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(wrapLength), "Wrap length must be greater than or equal to 0.");
            }

            Texture = new Texture(renderer, surface);

            IsWrapped = wrapLength > 0;
            Text = text;
            Font = font;
            Color = color;
            WrapLength = wrapLength;
        }

        public void UpdateText(string text)
        {
            UpdateText(text, 0);
        }

        public void UpdateText(string text, int wrapLength)
        {
            if (Texture == null)
            {
                throw new InvalidOperationException(Errors.E_TEXTURE_NULL);
            }

            ISurface surface = new Surface(Font, text, Color, wrapLength);
            Texture.UpdateSurfaceAndTexture(surface);

            Text = text;
            IsWrapped = wrapLength > 0;
        }

        public void SetOutlineSize(int outlineSize)
        {
            if (Font == null)
            {
                throw new InvalidOperationException(Errors.E_FONT_NULL);
            }

            if (outlineSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(outlineSize), "Outline size must be greater than or equal to 0.");
            }

            Font.SetOutlineSize(outlineSize);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Texture != null)
                {
                    Texture.Dispose();
                }

                if (Font != null)
                {
                    Font.Dispose();
                }
            }
        }
    }
}