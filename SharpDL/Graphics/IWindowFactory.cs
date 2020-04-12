namespace SharpDL.Graphics
{
    public interface IWindowFactory
    {
        /// <summary>Creates a Window used for display and rendering.
        /// </summary>
        /// <param name="title">String displayed in the Window title bar.</param>
        /// <returns>Instance of a Window.</returns>
        IWindow CreateWindow(string title);

        /// <summary>Creates a Window used for display and rendering.
        /// </summary>
        /// <param name="title">String displayed in the Window title bar.</param>
        /// <param name="x">X coordinate to position the Window.</param>
        /// <param name="y">Y coordinate to position the Window.</param>
        /// <returns>Instance of a Window.</returns>
        IWindow CreateWindow(string title, int x, int y);

        /// <summary>Creates a Window used for display and rendering.
        /// </summary>
        /// <param name="title">String displayed in the Window title bar.</param>
        /// <param name="x">X coordinate to position the Window.</param>
        /// <param name="y">Y coordinate to position the Window.</param>
        /// <param name="width">Width of the Window.</param>
        /// <param name="height">Height of the Window.</param>
        /// <returns>Instance of a Window.</returns>
        IWindow CreateWindow(string title, int x, int y, int width, int height);

        /// <summary>Creates a Window used for display and rendering.
        /// </summary>
        /// <param name="title">String displayed in the Window title bar.</param>
        /// <param name="x">X coordinate to position the Window.</param>
        /// <param name="y">Y coordinate to position the Window.</param>
        /// <param name="width">Width of the Window.</param>
        /// <param name="height">Height of the Window.</param>
        /// <param name="flags">Flags to give special behaviors and features to the Window.</param>
        /// <returns>Instance of a Window.</returns>
        IWindow CreateWindow(string title, int x, int y, int width, int height, WindowFlags flags);
    }
}