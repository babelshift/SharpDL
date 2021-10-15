namespace SharpDL.Graphics
{
    public interface ITextureFactory
    {
        /// <summary>Creates a texture with a specific width and height. Useful for render targets.
        /// </summary>
        /// <param name="renderer">Renderer from which textures will be drawn</param>
        /// <param name="width">Width of the render target</param>
        /// <param name="height">Height of the render target</param>
        /// <returns>GPU accelerated texture</returns>
        ITexture CreateTexture(IRenderer renderer, int width, int height);

        /// <summary>Creates a texture with a specific width, height, pixel format, and access mode. Useful for render targets.
        /// </summary>
        /// <param name="renderer">Renderer from which textures will be drawn</param>
        /// <param name="width">Width of the render target</param>
        /// <param name="height">Height of the render target</param>
        /// <param name="pixelFormat">Indicates the format in which pixels are represented such as RGB, RGBA, BGRA, etc...</param>
        /// <param name="accessMode">Texture access pattern such as static, streaming, and target. Use this to indicate to the GPU
        /// how often your texture will be changing.</param>
        /// <returns>GPU accelerated texture</returns>
        ITexture CreateTexture(IRenderer renderer, int width, int height, PixelFormat pixelFormat, TextureAccessMode accessMode);

        /// <summary>Creates a texture from a surface to be drawn with a specific renderer.
        /// </summary>
        /// <param name="renderer">Renderer from which texture will be drawn</param>
        /// <param name="surface">Surface from which texture will be created</param>
        /// <returns>GPU accelerated texture</returns>
        ITexture CreateTexture(IRenderer renderer, ISurface surface);
    }
}