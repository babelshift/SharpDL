using SharpDL.Shared;
using System;

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

        public TrueTypeText(IRenderer renderer, Surface surface, string text, Font textFont, Color color, int wrapLength)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException("renderer", Errors.E_RENDERER_NULL);
            }
            if (surface == null)
            {
                throw new ArgumentNullException("surface", Errors.E_SURFACE_NULL);
            }
            if (textFont == null)
            {
                throw new ArgumentNullException("textFont", Errors.E_FONT_NULL);
            }

            Text = text;
            Font = textFont;
            Color = color;
            WrapLength = wrapLength;
            if (wrapLength > 0)
                IsWrapped = true;
            else
                IsWrapped = false;
            Texture = new Texture(renderer, surface);
        }

        public void UpdateText(string text, int wrapLength = 0)
        {
            if(Texture == null)
            {
                throw new InvalidOperationException(Errors.E_TEXTURE_NULL);
            }

            Text = text;

            Surface surface = new Surface(Font, Text, Color, wrapLength);
            Texture.UpdateSurfaceAndTexture(surface);
        }

        public void SetOutlineSize(int outlineSize)
        {
            if (Font == null)
            {
                throw new InvalidOperationException(Errors.E_FONT_NULL);
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