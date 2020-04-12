using Microsoft.Win32.SafeHandles;
using SDL2;
using System;

namespace SharpDL.Graphics
{
    internal class SafeSurfaceHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeSurfaceHandle(IntPtr handle) : base(true)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            SDL.SDL_FreeSurface(handle);
            return true;
        }
    }
}