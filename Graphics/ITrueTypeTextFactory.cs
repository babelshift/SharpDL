namespace SharpDL.Graphics
{
    public interface ITrueTypeTextFactory
    {
        /// <summary>Creates a TTF element to render words from a font.
        /// </summary>
        /// <param name="renderer">Renderer from which the text will be drawn</param>
        /// <param name="fontPath">Path of the font to load for the text</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="fontSize">Size of the font to be drawn</param>
        /// <returns>TTF element to be rendered</returns>
        ITrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, string text, int fontSize);

        /// <summary>Creates a TTF element to render words from a font.
        /// </summary>
        /// <param name="renderer">Renderer from which the text will be drawn</param>
        /// <param name="fontPath">Path of the font to load for the text</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="fontSize">Size of the font to be drawn</param>
        /// <param name="color">Color of the text to be drawn</param>
        /// <returns>TTF element to be rendered</returns>
        ITrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, string text, int fontSize, Color color);

        /// <summary>Creates a TTF element to render words from a font. Supports text wrapping.
        /// </summary>
        /// <param name="renderer">Renderer from which the text will be drawn</param>
        /// <param name="fontPath">Path of the font to load for the text</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="fontSize">Size of the font to be drawn</param>
        /// <param name="color">Color of the text to be drawn</param>
        /// <param name="wrapLength">Wrap the text at a specific width (line breaks)</param>
        /// <returns>TTF element to be rendered</returns>
        ITrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, string text, int fontSize, Color color, int wrapLength, FontSurfaceType fontSurfaceType = FontSurfaceType.Blended);
    }
}