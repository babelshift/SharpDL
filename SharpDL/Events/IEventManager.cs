using System;
using SDL2;

namespace SharpDL.Events
{
    public interface IEventManager
    {
        event EventHandler<MouseWheelEventArgs> MouseWheelScrolling;
        event EventHandler<MouseButtonEventArgs> MouseButtonPressed;
        event EventHandler<MouseButtonEventArgs> MouseButtonReleased;
        event EventHandler<MouseMotionEventArgs> MouseMoving;
        event EventHandler<TextInputEventArgs> TextInputting;
        event EventHandler<TextEditingEventArgs> TextEditing;
        event EventHandler<KeyboardEventArgs> KeyPressed;
        event EventHandler<KeyboardEventArgs> KeyReleased;
        event EventHandler<VideoDeviceSystemEventArgs> VideoDeviceSystemEvent;
        event EventHandler<QuitEventArgs> Quitting;
        event EventHandler<EventArgs> Exiting;
        event EventHandler<WindowEventArgs> WindowShown;
        event EventHandler<WindowEventArgs> WindowHidden;
        event EventHandler<WindowEventArgs> WindowExposed;
        event EventHandler<WindowEventArgs> WindowMoved;
        event EventHandler<WindowEventArgs> WindowResized;
        event EventHandler<WindowEventArgs> WindowSizeChanged;
        event EventHandler<WindowEventArgs> WindowMinimized;
        event EventHandler<WindowEventArgs> WindowMaximized;
        event EventHandler<WindowEventArgs> WindowRestored;
        event EventHandler<WindowEventArgs> WindowEntered;
        event EventHandler<WindowEventArgs> WindowLeave;
        event EventHandler<WindowEventArgs> WindowFocusGained;
        event EventHandler<WindowEventArgs> WindowFocusLost;
        event EventHandler<WindowEventArgs> WindowClosed;
        void RaiseExiting(object sender, EventArgs args);
        void RaiseEvent(SDL.SDL_Event rawEvent);
    }
}