﻿using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public abstract class GameEventArgs : EventArgs
	{
		public enum GameEventType : uint
		{
			None = 0,
			Quit = SDL.SDL_EventType.SDL_QUIT,
			WindowEvent = SDL.SDL_EventType.SDL_WINDOWEVENT,
			VideoDeviceSystemEvent = SDL.SDL_EventType.SDL_SYSWMEVENT,
			KeyDown = SDL.SDL_EventType.SDL_KEYDOWN,
			KeyUp = SDL.SDL_EventType.SDL_KEYUP,
			TextEditing = SDL.SDL_EventType.SDL_TEXTEDITING,
			TextInput = SDL.SDL_EventType.SDL_TEXTINPUT,
			MouseMotion = SDL.SDL_EventType.SDL_MOUSEMOTION,
			MouseButtonDown = SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN,
			MouseButtonUp = SDL.SDL_EventType.SDL_MOUSEBUTTONUP,
			MouseWheel = SDL.SDL_EventType.SDL_MOUSEWHEEL,
			JoystickAxisMotion = SDL.SDL_EventType.SDL_JOYAXISMOTION,
			JoystickBallMotion = SDL.SDL_EventType.SDL_JOYBALLMOTION,
			JoystickHatMotion = SDL.SDL_EventType.SDL_JOYHATMOTION,
			JoystickButtonDown = SDL.SDL_EventType.SDL_JOYBUTTONDOWN,
			JoystickButtonUp = SDL.SDL_EventType.SDL_JOYBUTTONUP,
			JoystickDeviceAdded = SDL.SDL_EventType.SDL_JOYDEVICEADDED,
			JoystickDeviceRemoved = SDL.SDL_EventType.SDL_JOYDEVICEREMOVED,
			ControllerAxisMotion = SDL.SDL_EventType.SDL_CONTROLLERAXISMOTION,
			ControllerButtonDown = SDL.SDL_EventType.SDL_CONTROLLERBUTTONDOWN,
			ControllerButtonUp = SDL.SDL_EventType.SDL_CONTROLLERBUTTONUP,
			ControllerDeviceAdded = SDL.SDL_EventType.SDL_CONTROLLERDEVICEADDED,
			ControllerDeviceRemoved = SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMOVED,
			ControllerDeviceRemapped = SDL.SDL_EventType.SDL_CONTROLLERDEVICEREMAPPED,
			FingerDown = SDL.SDL_EventType.SDL_FINGERDOWN,
			FingerMotion = SDL.SDL_EventType.SDL_FINGERMOTION,
			DollarGesture = SDL.SDL_EventType.SDL_DOLLARGESTURE,
			DollarRecord = SDL.SDL_EventType.SDL_DOLLARRECORD,
			MultiGesture = SDL.SDL_EventType.SDL_MULTIGESTURE,
			ClipboardUpdate = SDL.SDL_EventType.SDL_CLIPBOARDUPDATE,
			DropFile = SDL.SDL_EventType.SDL_DROPFILE,
			UserEvent = SDL.SDL_EventType.SDL_USEREVENT
		}

		protected SDL.SDL_Event RawEvent { get; private set; }
		protected uint RawTimeStamp { get; set; }

		public GameEventType EventType { get; private set; }
		public TimeSpan TimeStamp { get { return new TimeSpan(RawTimeStamp); } }

		public GameEventArgs(SDL.SDL_Event rawEvent)
		{
			RawEvent = rawEvent;
			EventType = (GameEventType)rawEvent.type;
		}
	}
}