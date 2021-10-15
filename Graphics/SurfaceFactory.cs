using System;
using Microsoft.Extensions.Logging;

namespace SharpDL.Graphics
{
    public class SurfaceFactory : ISurfaceFactory
    {
        private readonly ILogger<SurfaceFactory> logger;

        public SurfaceFactory(
            ILogger<SurfaceFactory> logger = null)
        {
            this.logger = logger;
        }

        public ISurface CreateSurface(string filePath, SurfaceType surfaceType)
        {
            try
            {
                var surface = new Surface(filePath, surfaceType);
                logger?.LogTrace($"Surface created. Width = {surface.Width}, Height = {surface.Height}, FilePath = {surface.FilePath}, SurfaceType = {surface.Type}, Handle = {surface.Handle}.");
                return surface;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        public ISurface CreateSurface(IFont font, string text)
        {
            return CreateSurface(font, text, Color.Black, 0);
        }

        public ISurface CreateSurface(IFont font, string text, Color color)
        {
            return CreateSurface(font, text, color, 0);
        }

        public ISurface CreateSurface(IFont font, string text, Color color, int wrapLength)
        {
            try
            {
                var surface = new Surface(font, text, color, wrapLength);
                logger?.LogTrace($"Surface created for font. Width = {surface.Width}, Height = {surface.Height}, FilePath = {surface.FilePath}, SurfaceType = {surface.Type}, Handle = {surface.Handle}.");
                return surface;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}