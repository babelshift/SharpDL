using System;
using SDL2;
using SharpDL.Input;

namespace SharpDL.Events
{
    public class EventManager
    {
        public event EventHandler<MouseWheelEventArgs> MouseWheelScrolling;

        public event EventHandler<MouseButtonEventArgs> MouseButtonPressed;

        public event EventHandler<MouseButtonEventArgs> MouseButtonReleased;

        public event EventHandler<MouseMotionEventArgs> MouseMoving;

        public event EventHandler<TextInputEventArgs> TextInputting;

        public event EventHandler<TextEditingEventArgs> TextEditing;

        public event EventHandler<KeyboardEventArgs> KeyPressed;

        public event EventHandler<KeyboardEventArgs> KeyReleased;

        public event EventHandler<VideoDeviceSystemEventArgs> VideoDeviceSystemEvent;

        public event EventHandler<QuitEventArgs> Quitting;

        public event EventHandler<EventArgs> Exiting;

        public event EventHandler<WindowEventArgs> WindowShown;

        public event EventHandler<WindowEventArgs> WindowHidden;

        public event EventHandler<WindowEventArgs> WindowExposed;

        public event EventHandler<WindowEventArgs> WindowMoved;

        public event EventHandler<WindowEventArgs> WindowResized;

        public event EventHandler<WindowEventArgs> WindowSizeChanged;

        public event EventHandler<WindowEventArgs> WindowMinimized;

        public event EventHandler<WindowEventArgs> WindowMaximized;

        public event EventHandler<WindowEventArgs> WindowRestored;

        public event EventHandler<WindowEventArgs> WindowEntered;

        public event EventHandler<WindowEventArgs> WindowLeave;

        public event EventHandler<WindowEventArgs> WindowFocusGained;

        public event EventHandler<WindowEventArgs> WindowFocusLost;

        public event EventHandler<WindowEventArgs> WindowClosed;

        /// <summary>Raises the Exiting event. Exiting occurs when an unrecoverable exception occurs or the
        /// user directly exits the game.
        /// /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        internal void RaiseExiting(object sender, EventArgs args)
        {
            RaiseEvent(Exiting, args);
        }

        internal void RaiseEvent(SDL.SDL_Event rawEvent)
        {
            var eventType = (GameEventType)rawEvent.type;
            switch(eventType)
            {
                case GameEventType.First:
                    return;
                case GameEventType.Window:
                    var windowEventType = (WindowEventType)rawEvent.window.windowEvent;
                    switch(windowEventType)
                    {
                        case WindowEventType.Close: RaiseEvent(WindowClosed, rawEvent); break;
                        case WindowEventType.Enter: RaiseEvent(WindowEntered, rawEvent); break;
                        case WindowEventType.Exposed: RaiseEvent(WindowExposed, rawEvent); break;
                        case WindowEventType.FocusGained: RaiseEvent(WindowFocusGained, rawEvent); break;
                        case WindowEventType.FocusLost: RaiseEvent(WindowFocusLost, rawEvent); break;
                        case WindowEventType.Hidden: RaiseEvent(WindowHidden, rawEvent); break;
                        case WindowEventType.Leave: RaiseEvent(WindowLeave, rawEvent); break;
                        case WindowEventType.Maximized: RaiseEvent(WindowMaximized, rawEvent); break;
                        case WindowEventType.Minimized: RaiseEvent(WindowMinimized, rawEvent); break;
                        case WindowEventType.Moved: RaiseEvent(WindowMoved, rawEvent); break;
                        case WindowEventType.Resized: RaiseEvent(WindowResized, rawEvent); break;
                        case WindowEventType.Restored: RaiseEvent(WindowRestored, rawEvent); break;
                        case WindowEventType.Shown: RaiseEvent(WindowShown, rawEvent); break;
                        case WindowEventType.SizeChanged: RaiseEvent(WindowSizeChanged, rawEvent); break;
                    }
                    break;
                case GameEventType.Quit:
                    RaiseEvent(Quitting, rawEvent); break;
                case GameEventType.VideoDeviceSystemEvent:
                    RaiseEvent(VideoDeviceSystemEvent, rawEvent); break;
                case GameEventType.TextEditing: 
                    RaiseEvent(TextEditing, rawEvent); break;
                case GameEventType.TextInput:
                    RaiseEvent(TextInputting, rawEvent); break;
                case GameEventType.KeyDown:
                case GameEventType.KeyUp:
                    var keyState = (KeyState)rawEvent.key.state;
                    if (keyState == KeyState.Pressed)
                        RaiseEvent(KeyPressed, rawEvent);
                    else if (keyState == KeyState.Released)
                        RaiseEvent(KeyReleased, rawEvent);
                    break;
                case GameEventType.MouseMotion:
                    Mouse.UpdateMousePosition(rawEvent.motion.x, rawEvent.motion.y);
                    RaiseEvent(MouseMoving, rawEvent);
                    break;
                case GameEventType.MouseButtonDown:
                case GameEventType.MouseButtonUp:
                    var mouseButtonState = (MouseButtonState)rawEvent.button.state;
                    if (mouseButtonState == MouseButtonState.Pressed)
                        RaiseEvent(MouseButtonPressed, rawEvent);
                    else if (mouseButtonState == MouseButtonState.Released)
                        RaiseEvent(MouseButtonReleased, rawEvent);
                    break;
                case GameEventType.MouseWheel:
                    RaiseEvent(MouseWheelScrolling, rawEvent); break;
            }
        }

        /// <summary>Raises an event handler with arguments parsed from the SDL event parameter.
        /// This method does nothing if there are no event subscribers.
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="rawEvent"></param>
        /// <typeparam name="T"></typeparam>
        private void RaiseEvent<T>(EventHandler<T> eventHandler, SDL.SDL_Event rawEvent)
            where T : EventArgs
        {
            var eventArgs = CreateEventArgs<T>(rawEvent);
            RaiseEvent(eventHandler, eventArgs);
        }

        /// <summary>Raises an event handler using the event arguments parameter.
        /// This method does nothing if there are no event subscribers.
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="eventArgs"></param>
        /// <typeparam name="T"></typeparam>
        private void RaiseEvent<T>(EventHandler<T> eventHandler, T eventArgs)
            where T : EventArgs
        {
            if (eventHandler != null)
            {
                eventHandler(this, eventArgs);
            }
        }

		private static T CreateEventArgs<T>(SDL.SDL_Event rawEvent)
            where T : class
		{
			return Activator.CreateInstance(typeof(T),
				new object[] { rawEvent }) as T;
		}
    }
}