using Microsoft.Win32.SafeHandles;
using SDL2;
using System;

namespace SharpDL.Graphics
{
    internal class SafeTextureHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeTextureHandle(IntPtr handle) : base(true)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            SDL.SDL_DestroyTexture(handle);
            return true;
        }
    }
}