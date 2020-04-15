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

        public ITrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, int fontSize, Color color, string text, int wrapLength)
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
                surface = new Surface(font, text, color, wrapLength);
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