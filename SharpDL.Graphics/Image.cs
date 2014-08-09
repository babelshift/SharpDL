using SharpDL.Shared;
using System;

namespace SharpDL.Graphics
{
    public class Image : IDisposable
    {
        public ImageFormat Format { get; private set; }

        public Texture Texture { get; private set; }

        public Image(Renderer renderer, Surface surface, ImageFormat imageFormat)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException("renderer", Errors.E_RENDERER_NULL);
            }
            if (surface == null)
            {
                throw new ArgumentNullException("surface", Errors.E_SURFACE_NULL);
            }

            if (surface.Type == SurfaceType.Text)
            {
                throw new InvalidOperationException("Cannot create images from text surfaces.");
            }

            Format = imageFormat;

            if (surface.Type == SurfaceType.BMP)
                Format = ImageFormat.BMP;
            else if (surface.Type == SurfaceType.PNG)
                Format = ImageFormat.PNG;
            else if (surface.Type == SurfaceType.JPG)
                Format = ImageFormat.JPG;

            Texture = new Texture(renderer, surface);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Texture != null)
            {
                Texture.Dispose();
            }
        }
    }
}