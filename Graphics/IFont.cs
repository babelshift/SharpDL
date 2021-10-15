using System;

namespace SharpDL.Graphics
{
    public interface IFont : IDisposable
    {
        /// <summary>File path from which the Font's content was loaded.
        /// </summary>
        string FilePath { get; }

        /// <summary>Font size upon final render based on 72DPI (basically translates to pixel height).
        /// </summary>
        int PointSize { get; }

        /// <summary>Unsafe native handle returned by the underlying graphics API. This will probably
        /// be removed from the interface in future versions.
        /// </summary>
        IntPtr Handle { get; }

        /// <summary>Outline pixel size upon findal rendering.
        /// </summary>
        int OutlineSize { get; }

        /// <summary>Sets the font's outline pixel size.
        /// </summary>
        /// <param name="outlineSize">Outline size in pixels. Set to 0 to disable outline.</param>
        void SetOutlineSize(int outlineSize);
    }
}