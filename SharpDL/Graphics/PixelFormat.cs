using SDL2;

namespace SharpDL.Graphics
{
    public enum PixelFormat
    {
        Unknown,
        RGBA5551,
        ARGB1555,
        BGRA4444,
        ABGR4444,
        RGBA4444,
        ARGB4444,
        BGR555,
        RGB555,
        ABGR1555,
        BGR444,
        RGB332,
        INDEX8,
        INDEX4MSB,
        INDEX4LSB,
        INDEX1MSB,
        INDEX1LSB,
        RGB444,
        BGRA5551,
        BGR565,
        RGB565,
        YVYU,
        YUY2,
        YV12,
        ARGB2101010,
        BGRA8888,
        UYVY,
        RGBA8888,
        ARGB8888,
        RGB24,
        BGRX8888,
        BGR24,
        BGR888,
        RGBX8888,
        RGB888,
        ABGR8888

    }

    /// <summary>
    /// This exists to map from our enum representation of a Pixel Format to SDL's representation.
    /// We can't directly set the values of the SDL pixel formats to our enum values because
    /// they are readonly values instead of consts.
    /// </summary>
    internal static class PixelFormatMap
    {
        /// <summary>
        /// Converts SDL uint to engine enum. Must be implemented as if/else instead of switch because
        /// case statements cannot have non-constant values.
        /// </summary>
        /// <param name="pixelFormat">Pixel format value coming from SDL</param>
        /// <returns>Engine representation of pixel format as enum</returns>
        public static PixelFormat SDLToEnum(uint pixelFormat)
        {
            if (pixelFormat == SDL.SDL_PIXELFORMAT_ABGR1555) { return PixelFormat.ABGR1555; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_ABGR4444) { return PixelFormat.ABGR4444; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_ARGB2101010) { return PixelFormat.ARGB2101010; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_ARGB4444) { return PixelFormat.ARGB4444; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_ARGB8888) { return PixelFormat.ARGB8888; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_BGR24) { return PixelFormat.BGR24; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_BGR444) { return PixelFormat.BGR444; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_BGR555) { return PixelFormat.BGR555; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_BGR565) { return PixelFormat.BGR565; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_BGR888) { return PixelFormat.BGR888; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_BGRA4444) { return PixelFormat.BGRA4444; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_BGRA5551) { return PixelFormat.BGRA5551; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_BGRA8888) { return PixelFormat.BGRA8888; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_BGRX8888) { return PixelFormat.BGRX8888; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_INDEX1LSB) { return PixelFormat.INDEX1LSB; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_INDEX1MSB) { return PixelFormat.INDEX1MSB; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_INDEX4LSB) { return PixelFormat.INDEX4LSB; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_INDEX4MSB) { return PixelFormat.INDEX4MSB; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_INDEX8) { return PixelFormat.INDEX8; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGB24) { return PixelFormat.RGB24; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGB332) { return PixelFormat.RGB332; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGB444) { return PixelFormat.RGB444; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGB555) { return PixelFormat.RGB555; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGB565) { return PixelFormat.RGB565; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGB888) { return PixelFormat.RGB888; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGBA4444) { return PixelFormat.RGBA4444; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGBA5551) { return PixelFormat.RGBA5551; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGBA8888) { return PixelFormat.RGBA8888; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_RGBX8888) { return PixelFormat.RGBX8888; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_UNKNOWN) { return PixelFormat.Unknown; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_UYVY) { return PixelFormat.UYVY; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_YUY2) { return PixelFormat.YUY2; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_YV12) { return PixelFormat.YV12; }
            if (pixelFormat == SDL.SDL_PIXELFORMAT_YVYU) { return PixelFormat.YVYU; }
            else return PixelFormat.Unknown;
        }

        public static uint EnumToSDL(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.ABGR1555: return SDL.SDL_PIXELFORMAT_ABGR1555;
                case PixelFormat.ABGR4444: return SDL.SDL_PIXELFORMAT_ABGR4444;
                case PixelFormat.ABGR8888: return SDL.SDL_PIXELFORMAT_ABGR8888;
                case PixelFormat.ARGB1555: return SDL.SDL_PIXELFORMAT_ARGB1555;
                case PixelFormat.ARGB2101010: return SDL.SDL_PIXELFORMAT_ARGB2101010;
                case PixelFormat.ARGB4444: return SDL.SDL_PIXELFORMAT_ARGB4444;
                case PixelFormat.ARGB8888: return SDL.SDL_PIXELFORMAT_ARGB8888;
                case PixelFormat.BGR24: return SDL.SDL_PIXELFORMAT_BGR24;
                case PixelFormat.BGR444: return SDL.SDL_PIXELFORMAT_BGR444;
                case PixelFormat.BGR555: return SDL.SDL_PIXELFORMAT_BGR555;
                case PixelFormat.BGR565: return SDL.SDL_PIXELFORMAT_BGR565;
                case PixelFormat.BGR888: return SDL.SDL_PIXELFORMAT_BGR888;
                case PixelFormat.BGRA4444: return SDL.SDL_PIXELFORMAT_BGRA4444;
                case PixelFormat.BGRA5551: return SDL.SDL_PIXELFORMAT_BGRA5551;
                case PixelFormat.BGRA8888: return SDL.SDL_PIXELFORMAT_BGRA8888;
                case PixelFormat.BGRX8888: return SDL.SDL_PIXELFORMAT_BGRX8888;
                case PixelFormat.INDEX1LSB: return SDL.SDL_PIXELFORMAT_INDEX1LSB;
                case PixelFormat.INDEX1MSB: return SDL.SDL_PIXELFORMAT_INDEX1MSB;
                case PixelFormat.INDEX4LSB: return SDL.SDL_PIXELFORMAT_INDEX4LSB;
                case PixelFormat.INDEX4MSB: return SDL.SDL_PIXELFORMAT_INDEX4MSB;
                case PixelFormat.INDEX8: return SDL.SDL_PIXELFORMAT_INDEX8;
                case PixelFormat.RGB24: return SDL.SDL_PIXELFORMAT_RGB24;
                case PixelFormat.RGB332: return SDL.SDL_PIXELFORMAT_RGB332;
                case PixelFormat.RGB444: return SDL.SDL_PIXELFORMAT_RGB444;
                case PixelFormat.RGB555: return SDL.SDL_PIXELFORMAT_RGB555;
                case PixelFormat.RGB565: return SDL.SDL_PIXELFORMAT_RGB565;
                case PixelFormat.RGB888: return SDL.SDL_PIXELFORMAT_RGB888;
                case PixelFormat.RGBA4444: return SDL.SDL_PIXELFORMAT_RGBA4444;
                case PixelFormat.RGBA5551: return SDL.SDL_PIXELFORMAT_RGBA5551;
                case PixelFormat.RGBA8888: return SDL.SDL_PIXELFORMAT_RGBA8888;
                case PixelFormat.RGBX8888: return SDL.SDL_PIXELFORMAT_RGBX8888;
                case PixelFormat.Unknown: return SDL.SDL_PIXELFORMAT_UNKNOWN;
                case PixelFormat.UYVY: return SDL.SDL_PIXELFORMAT_UYVY;
                case PixelFormat.YUY2: return SDL.SDL_PIXELFORMAT_YUY2;
                case PixelFormat.YV12: return SDL.SDL_PIXELFORMAT_YV12;
                case PixelFormat.YVYU: return SDL.SDL_PIXELFORMAT_YVYU;
                default: return SDL.SDL_PIXELFORMAT_UNKNOWN;
            }
        }
    }
}