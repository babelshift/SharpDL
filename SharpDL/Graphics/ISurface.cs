using System;

namespace SharpDL.Graphics
{
    public interface ISurface : IDisposable
    {
        string FilePath { get; }
        int Width { get; }
        int Height { get; }
        SurfaceType Type { get; }
        IntPtr Handle { get; }
    }
}