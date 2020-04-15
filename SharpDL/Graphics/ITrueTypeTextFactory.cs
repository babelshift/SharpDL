namespace SharpDL.Graphics
{
    public interface ITrueTypeTextFactory
    {
        ITrueTypeText CreateTrueTypeText(IRenderer renderer, string fontPath, int fontSize, Color color, string text, int wrapLength);
    }
}