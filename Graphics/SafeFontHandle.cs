using System;
using Microsoft.Win32.SafeHandles;
using SDL2;

namespace SharpDL.Graphics
{
    internal class SafeFontHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeFontHandle(IntPtr handle) : base(true)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            SDL_ttf.TTF_CloseFont(handle);
            return true;
        }
    }
}