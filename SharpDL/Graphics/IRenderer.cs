using System;
using System.Collections.Generic;

namespace SharpDL.Graphics
{
    public interface IRenderer : IDisposable
    {
        /// <summary>Window in which this Renderer was created and will display to.
        /// </summary>
        IWindow Window { get; }

        /// <summary>The index of the rendering driver to initialize, or -1 to initialize the first one supporting the requested flags.
        /// </summary>
        int Index { get; }

        /// <summary>Initial behavior flags of the Renderer upon creation.
        /// </summary>
        IEnumerable<RendererFlags> Flags { get; }

        /// <summary>Native pointer to Renderer as stored in memory by SDL2. TODO: should this be on the interface?
        /// </summary>
        IntPtr Handle { get; }

        /// <summary>Clears the screen to whatever color is set in the Renderer.
        /// </summary>
        void ClearScreen();

        /// <summary>Updates the Window with any rendering committed to the Renderer.
        /// </summary>
        void RenderPresent();

        /// <summary>Resets the Renderer's render target back to null.
        /// </summary>
        void ResetRenderTarget();

        /// <summary>Updates the Renderer's blend mode such as Alpha, Additive, or Modulation.
        /// </summary>
        /// <param name="blendMode">Blend mode to select.</param>
        void SetBlendMode(BlendMode blendMode);

        /// <summary>Updates the Renderer's current draw color to use with Rectangles, Lines, and Points.
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <param name="a">Alpha</param>
        void SetDrawColor(byte r, byte g, byte b, byte a);

        /// <summary>Sets the logical size in which the Renderer will draw. Useful when we want to render to a device independent resolution.
        /// </summary>
        /// <param name="width">Width of the logical size</param>
        /// <param name="height">Height of the logical size</param>
        void SetRenderLogicalSize(int width, int height);

        /// <summary>Updates the Renderer's current texture target for rendering operations.
        /// </summary>
        /// <param name="renderTarget">Texture to render to.</param>
        void SetRenderTarget(RenderTarget renderTarget);
    }
}