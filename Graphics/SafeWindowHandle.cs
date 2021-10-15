using System;
using Microsoft.Win32.SafeHandles;
using SDL2;

namespace SharpDL.Graphics
{
    internal class SafeWindowHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeWindowHandle(IntPtr handle) : base(true)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            SDL.SDL_DestroyWindow(handle);
            return true;
        }
    }
}