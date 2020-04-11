namespace SharpDL.Graphics
{
    public interface IRendererFactory
    {
        
        /// <summary>Creates a Renderer paired with a Window to perform rendering.
        /// </summary>
        /// <param name="window">The window where rendering is displayed</param>
        /// <returns>Instance of a Renderer.</returns>
        /// <remarks></remarks>
        Renderer CreateRenderer(Window window);

        /// <summary>Creates a Renderer paired with a Window to perform rendering.
        /// </summary>
        /// <param name="window">The window where rendering is displayed</param>
        /// <param name="index">The index of the rendering driver to initialize, or -1 to initialize the first one supporting the requested flags</param>
        /// <returns>Instance of a Renderer.</returns>
        Renderer CreateRenderer(Window window, int index);
        
        /// <summary>Creates a Renderer paired with a Window to perform rendering.
        /// </summary>
        /// <param name="window">The window where rendering is displayed</param>
        /// <param name="index">The index of the rendering driver to initialize, or -1 to initialize the first one supporting the requested flags</param>
        /// <param name="flags">0, or one or more RendererFlags OR'd together</param>
        /// <returns>Instance of a Renderer.</returns>
        Renderer CreateRenderer(Window window, int index, RendererFlags flags);
    }
}