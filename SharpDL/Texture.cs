using SDL2;
using System;

namespace SharpDL
{
	public class Texture : IDisposable
	{
		public uint Format { get; private set; }
		public int Access { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public Surface Surface { get; private set; }
		public Renderer Renderer { get; private set; }
		public IntPtr Handle { get; private set; }

		public Texture(Renderer renderer, Surface surface)
		{
			this.Renderer = renderer;
			this.Surface = surface;

			bool success = false;

			if (surface != null)
			{
				if (surface.Handle != null)
				{
					this.Handle = SDL.SDL_CreateTextureFromSurface(this.Renderer.Handle, this.Surface.Handle);
					if (this.Handle != null)
						success = true;
				}
			}

			if (!success)
				throw new Exception("SDL_CreateTextureFromSurface");
			else
			{
				this.Surface.Dispose();

				uint format;
				int access, width, height;
				SDL.SDL_QueryTexture(this.Handle, out format, out access, out width, out height);

				this.Format = format;
				this.Access = access;
				this.Width = width;
				this.Height = height;
			}
		}


		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~Texture()
		{
			Dispose(false);
		}

		private void Dispose(bool disposing)
		{
			SDL.SDL_DestroyTexture(this.Handle);
		}
	}
}
