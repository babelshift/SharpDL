using SharpDL.Shared;
using System;
namespace SharpDL.Graphics
{
    public static class TrueTypeTextFactory
    {
        public static TrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, int fontSize, Color color, string text, int wrapLength)
        {
            Font font = null;
            Surface surface = null;
            TrueTypeText trueTypeText = null;

            try
            {
                if (renderer == null)
                {
                    throw new ArgumentNullException("renderer", Errors.E_RENDERER_NULL);
                }
                if(String.IsNullOrEmpty(fontPath))
                {
                    throw new ArgumentNullException("fontPath", Errors.E_FONT_PATH_MISSING);
                }

                font = new Font(fontPath, fontSize);
                surface = new Surface(font, text, color, wrapLength);
                trueTypeText = new TrueTypeText(renderer, surface, text, font, color, wrapLength);
                return trueTypeText;
            }
            catch (Exception ex)
            {
                // something went wrong while initializing this true type text, clean everything up and throw up
                font.Dispose();
                surface.Dispose();
                trueTypeText.Dispose();
                throw ex;
            }
        }
    }
}