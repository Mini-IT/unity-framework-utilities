using System;

namespace MiniIT.Utils
{
	public interface ITickTimer
	{
		event Action OnTick;

		TimeSpan Interval { get; set; }
		bool IsRunning { get; }

		void Start();
		void Stop();
	}
}
