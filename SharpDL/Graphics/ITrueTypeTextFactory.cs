namespace SharpDL.Graphics
{
    public interface ITrueTypeTextFactory
    {
        /// <summary>
        /// Creates a TTF element from a font and text to be rendered with sizes and colors.
        /// </summary>
        /// <param name="renderer">Renderer from which the text will be drawn</param>
        /// <param name="fontPath">Path of the font to load for the text</param>
        /// <param name="fontSize">Size of the font to be drawn</param>
        /// <param name="color">Color of the text to be drawn</param>
        /// <param name="text">Text to be drawn</param>
        /// <param name="wrapLength">Wrap the text at a specific width (line breaks)</param>
        /// <returns>TTF element</returns>
        ITrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, int fontSize, Color color, string text, int wrapLength);
    }
}