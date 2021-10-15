using System;
using Microsoft.Win32.SafeHandles;
using SDL2;

namespace SharpDL.Graphics
{
    internal class SafeRendererHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeRendererHandle(IntPtr handle) : base(true)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            SDL.SDL_DestroyRenderer(handle);
            return true;
        }
    }
}