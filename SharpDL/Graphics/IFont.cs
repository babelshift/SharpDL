using System;

namespace SharpDL.Graphics
{
    public interface IFont : IDisposable
    {
        string FilePath { get; }
        int PointSize { get; }
        IntPtr Handle { get; }
        int OutlineSize { get; }
        void SetOutlineSize(int outlineSize);
    }
}