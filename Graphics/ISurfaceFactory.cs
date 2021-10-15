namespace SharpDL.Graphics
{
    public interface ISurfaceFactory
    {
        /// <summary>Creates a surface from an image on disk identified by file path and surface type.
        /// </summary>
        /// <param name="filePath">Where on disk to load the image</param>
        /// <param name="surfaceType">Type of image being loaded (PNG, BMP, JPG, etc...)</param>
        /// <returns>In memory representation of loaded image to render</returns>
        ISurface CreateSurface(string filePath, SurfaceType surfaceType);

        /// <summary>Creates a surface from a font to be rendered a specific color with optional wrapping (line breaks).
        /// </summary>
        /// <param name="font">Font to be used during rendering</param>
        /// <param name="text">Text to be rendered</param>
        /// <param name="color">Color of the text</param>
        /// <param name="wrapLength">Indicates if the text should wrap at a certain length</param>
        /// <returns>In memory representation of font/text to render</returns>
        ISurface CreateSurface(IFont font, string text, Color color, int wrapLength);
    }
}