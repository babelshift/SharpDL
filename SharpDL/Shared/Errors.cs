using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL.Shared
{
    public static class Errors
    {
        public const string E_RENDERER_NULL = "Attempted to use a null renderer. Maybe it was instantiated incorrectly or has been disposed?";

        public const string E_RENDERER_NO_RENDER_TARGET_SUPPORT = "This renderer does not support render targets. Did you create the renderer with the RendererFlags.SupportRenderTargets flag?";

        public const string E_TEXTURE_NULL = "Attempted to use a null texture. Maybe it was instantiated incorrectly or has been disposed?";

        public const string E_SURFACE_NULL = "Attempted to use a null surface. Maybe it was instantiated incorrectly or has been disposed?";

        public const string E_SURFACE_PATH_MISSING = "Surface path not supplied. Did you remember to point to the correct image file?";

        public const string E_WINDOW_NULL = "Attempted to use a null window. Maybe it was instantiated incorrectly or has been disposed?";

        public const string E_FONT_NULL = "Attempted to use a null font. Maybe it was instantiated incorrectly or has been disposed?";

        public const string E_FONT_PATH_MISSING = "Font path not supplied. Did you remember to point to the correct font file?";

        public const string E_RENDER_TARGET_NULL = "Attempted to use a null render target. Maybe it was instantiated incorrectly or has been disposed?";
    }
}
