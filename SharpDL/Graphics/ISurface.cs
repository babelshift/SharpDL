using System;

namespace SharpDL.Graphics
{
    public interface ISurface : IDisposable
    {
        /// <summary>File path from which the Surface's source content was loaded.
        /// </summary>
        string FilePath { get; }

        /// <summary>Width of the Surface's renderable area.
        /// </summary>
        int Width { get; }

        /// <summary>Height of the Surface's renderable area.
        /// </summary>
        int Height { get; }

        /// <summary>Indicates the type of Surface such as PNG, BMP, or JPG. 
        /// </summary>
        SurfaceType Type { get; }

        /// <summary>Unsafe native handle returned by the underlying graphics API. This will probably
        /// be removed from the interface in future versions.
        /// </summary>
        IntPtr Handle { get; }
    }
}