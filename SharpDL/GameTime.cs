using System;

namespace SharpDL
{
	public class GameTime
	{
		public TimeSpan TotalGameTime { get; set; }

		public TimeSpan ElapsedGameTime { get; set; }

		public GameTime()
		{
			TotalGameTime = TimeSpan.Zero;
			ElapsedGameTime = TimeSpan.Zero;
		}

		public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
		{
			TotalGameTime = totalGameTime;
			ElapsedGameTime = elapsedGameTime;
		}
	}
}
