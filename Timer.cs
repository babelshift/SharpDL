using SDL2;
using System;

namespace SharpDL
{
	public class Timer
	{
		private uint startedAtTicks = 0;
		private uint pausedTicks = 0;
		private bool isStarted = false;
		private bool isPaused = false;

		public TimeSpan StartedAtTime
		{
			get { return TimeSpan.FromMilliseconds((double)startedAtTicks); }
		}

		public TimeSpan ElapsedTime
		{
			get
			{
				if (isStarted)
				{
					if (isPaused)
						return TimeSpan.FromMilliseconds((double)pausedTicks);
					else
						return TimeSpan.FromMilliseconds((double)(SDL.SDL_GetTicks() - startedAtTicks));
				}
				else
					return TimeSpan.Zero;
			}
		}

		public bool IsStarted { get { return isStarted; } }

		public bool IsPaused { get { return isPaused; } }

		public void Start()
		{
			isStarted = true;
			isPaused = false;
			startedAtTicks = SDL.SDL_GetTicks();
		}

		public void Stop()
		{
			isStarted = false;
			isPaused = false;
		}

		public void Pause()
		{
			if (isStarted && !isPaused)
			{
				isPaused = true;
				pausedTicks = SDL.SDL_GetTicks() - startedAtTicks;
			}
		}

		public void Unpause()
		{
			if (isPaused)
			{
				isPaused = false;
				startedAtTicks = SDL.SDL_GetTicks() - pausedTicks;
				pausedTicks = 0;
			}
		}
	}
}
