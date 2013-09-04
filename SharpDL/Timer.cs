using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDL
{
	public class Timer
	{
		private UInt32 startTicks;
		private UInt32 pausedTicks;

		bool isStarted;
		bool isPaused;

		public TimeSpan StartTime
		{
			get { return TimeSpan.FromMilliseconds((double)startTicks); }
		}

		public TimeSpan ElapsedTime
		{
			get
			{
				if(isStarted)
				{
					if(isPaused)
						return TimeSpan.FromMilliseconds((double)pausedTicks);
					else
						return TimeSpan.FromMilliseconds((double)(SDL.SDL_GetTicks() - startTicks));
				}
				else 
					return TimeSpan.Zero;
			}
		}

		public bool IsStarted { get { return isStarted; } }

		public bool IsPaused { get { return isPaused; } }

		public Timer()
		{
			startTicks = 0;
			pausedTicks = 0;
			isPaused = false;
			isStarted = false;
		}

		public void Start()
		{
			isStarted = true;
			isPaused = false;
			startTicks = SDL.SDL_GetTicks();
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
				pausedTicks = SDL.SDL_GetTicks() - startTicks;
			}
		}

		public void Unpause()
		{
			if (isPaused)
			{
				isPaused = false;
				startTicks = SDL.SDL_GetTicks() - pausedTicks;
				pausedTicks = 0;
			}
		}
	}
}
