using System;
using System.Collections.Generic;

namespace SharpDL.Graphics
{
    public interface IWindow : IDisposable
    {
        /// <summary>Title of the Window upon creation.
        /// </summary>
        string Title { get; }

        /// <summary>Initial X coordinate of Window upon creation. Value does not update when Window is moved.
        /// </summary>
        int X { get; }

        /// <summary>Initial Y coordinate of Window upon creation. Value does not update when Window is moved.
        /// </summary>
        int Y { get; }

        /// <summary>Initial width of Window upon creation. Value does not update when Window is resized.
        /// </summary>
        int Width { get; }
        
        /// <summary>Initial height of Window upon creation. Value does not update when Window is resized.
        /// </summary>
        int Height { get; }
        
        /// <summary>Initial behavior flags of the Window upon creation.
        /// </summary>
        IEnumerable<WindowFlags> Flags { get; }
        
        /// <summary>Native pointer to Window as stored in memory by SDL2. TODO: should this be on the interface?
        /// </summary>
        IntPtr Handle { get; }
    }
}