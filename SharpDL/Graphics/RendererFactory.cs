using System;
using Microsoft.Extensions.Logging;
using SharpDL.Shared;

namespace SharpDL.Graphics
{
    public class RendererFactory : IRendererFactory
    {
        private readonly ILogger<RendererFactory> logger;
        private readonly ILogger<Renderer> loggerRenderer;

        public RendererFactory(
            ILogger<RendererFactory> logger = null, 
            ILogger<Renderer> loggerRenderer = null)
        {
            this.logger = logger;
            this.loggerRenderer = loggerRenderer;
        }

        public IRenderer CreateRenderer(Window window)
        {
            return CreateRenderer(window, -1, RendererFlags.None);
        }

        public IRenderer CreateRenderer(Window window, int index)
        {
            return CreateRenderer(window, index, RendererFlags.None);
        }

        public IRenderer CreateRenderer(Window window, int index, RendererFlags flags)
        {
            try
            {
                var renderer = new Renderer(window, index, flags, loggerRenderer);
                logger?.LogTrace($"Renderer created. Handle = {renderer.Handle}, Window Title = {window.Title}, Window Handle = {window.Handle}.");
                SDL2.SDL.SDL_SetHint(SDL2.SDL.SDL_HINT_RENDER_SCALE_QUALITY, "linear");
                return renderer;
            }
            catch(Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                throw;
            }
        }

    }
}