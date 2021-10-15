using System;
using Microsoft.Extensions.Logging;

namespace SharpDL.Graphics
{

    public class TrueTypeTextFactory : ITrueTypeTextFactory
    {
        private readonly ILogger<TrueTypeTextFactory> logger;

        public TrueTypeTextFactory(ILogger<TrueTypeTextFactory> logger)
        {
            this.logger = logger;
        }

        public ITrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, string text, int fontSize)
        {
            return CreateTrueTypeText(renderer, fontPath, text, fontSize, Color.Black, 0);
        }
        
        public ITrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, string text, int fontSize, Color color)
        {
            return CreateTrueTypeText(renderer, fontPath, text, fontSize, color, 0);
        }

        public ITrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, string text, int fontSize, Color color, int wrapLength, FontSurfaceType fontSurfaceType = FontSurfaceType.Blended)
        {
            Font font = null;
            ISurface surface = null;
            ITrueTypeText trueTypeText = null;

            if (renderer == null)
            {
                throw new ArgumentNullException(nameof(renderer));
            }

            if (String.IsNullOrWhiteSpace(fontPath))
            {
                throw new ArgumentNullException(nameof(fontPath));
            }

            if (fontSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(fontSize), "Font size must be greater than 0.");
            }

            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            try
            {
                font = new Font(fontPath, fontSize);
                surface = new Surface(font, text, color, wrapLength, fontSurfaceType);
                trueTypeText = new TrueTypeText(renderer, surface, text, font, color, wrapLength);
                logger?.LogTrace($"TrueTypeText created. Width = {trueTypeText.Texture.Width}, Height = {trueTypeText.Texture.Height}, Font = {trueTypeText.Font.FilePath}, WrapLength = {trueTypeText.WrapLength}.");
                return trueTypeText;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while creating a TrueTypeText objecte.");
                font.Dispose();
                surface.Dispose();
                trueTypeText.Dispose();
                throw;
            }
        }
    }
}