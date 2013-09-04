using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public class Image : IDisposable
	{
		public enum ImageFormat
		{
			BMP,
			PNG,
			JPG
		}

		public ImageFormat Format { get; private set; }
		public Texture Texture { get; private set; }

		public Image(Renderer renderer, Surface surface, ImageFormat imageFormat)
		{
			if (surface.Type == SharpDL.Surface.SurfaceType.Text)
				throw new Exception("Cannot create images from text surfaces.");

			Format = imageFormat;

			if (surface.Type == SharpDL.Surface.SurfaceType.BMP)
				Format = ImageFormat.BMP;
			else if (surface.Type == SharpDL.Surface.SurfaceType.PNG)
				Format = ImageFormat.PNG;
			else if (surface.Type == SharpDL.Surface.SurfaceType.JPG)
				Format = ImageFormat.JPG;

			Texture = new Texture(renderer, surface);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Image()
		{
			Dispose(false);
		}

		private void Dispose(bool disposing)
		{
			Texture.Dispose();
		}
	}
}
