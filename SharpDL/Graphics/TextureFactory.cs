using System;
using Microsoft.Extensions.Logging;

namespace SharpDL.Graphics
{
    public class TextureFactory : ITextureFactory
    {
        private readonly ILogger<TextureFactory> logger;

        public TextureFactory(
            ILogger<TextureFactory> logger = null)
        {
            this.logger = logger;
        }

        public ITexture CreateTexture(IRenderer renderer, int width, int height)
        {
            return CreateTexture(renderer, width, height, PixelFormat.RGBA8888, TextureAccessMode.Static);
        }
        
        public ITexture CreateTexture(IRenderer renderer, int width, int height, PixelFormat pixelFormat, TextureAccessMode accessMode)
        {
            try
            {
                var texture = new Texture(renderer, width, height, pixelFormat, accessMode);
                logger?.LogTrace($"Texture created. Width = {texture.Width}, Height = {texture.Height}, PixelFormat = {texture.PixelFormat}, AccessMode = {texture.AccessMode}, Handle = {texture.Handle}.");
                return texture;
            }
            catch(Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        public ITexture CreateTexture(IRenderer renderer, ISurface surface)
        {
            try
            {
                var texture = new Texture(renderer, surface);
                logger?.LogTrace($"Texture created from surface. Width = {texture.Width}, Height = {texture.Height}, PixelFormat = {texture.PixelFormat}, AccessMode = {texture.AccessMode}, Handle = {texture.Handle}.");
                return texture;
            }
            catch(Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}