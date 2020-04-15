using System;

namespace SharpDL.Graphics
{
    public interface ITrueTypeText : IDisposable
    {
        string Text { get; }
        IFont Font { get; }
        Color Color { get; }
        ITexture Texture { get; }
        int OutlineSize { get; }
        bool IsWrapped { get; set; }
        int WrapLength { get; set; }
        void SetOutlineSize(int outlineSize);
        void UpdateText(string text);
        void UpdateText(string text, int wrapLength);
    }
}