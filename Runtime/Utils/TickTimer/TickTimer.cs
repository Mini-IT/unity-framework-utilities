using System;

namespace MiniIT.Utils
{
	public sealed class TickTimer : ITickTimer
	{
		public event Action OnTick;

		public TimeSpan Interval
		{
			get => _interval;
			set
			{
				if (_interval != value)
				{
					_lastTickTime = DateTime.UtcNow;
					_interval = value;
				}
			}
		}

		public bool IsRunning { get; private set; }

		private TimeSpan _interval;
		private DateTime _lastTickTime;

		internal TickTimer(TimeSpan interval)
		{
			Interval = interval;
		}

		public void Start()
		{
			IsRunning = true;
		}

		public void Stop()
		{
			IsRunning = false;
		}

		internal void InvokeTick(DateTime now)
		{
			OnTick?.Invoke();
			_lastTickTime = now;
		}

		internal TimeSpan GetTimeFromLastTick(long nowTicks)
		{
			return Stopwatch.GetElapsedTime(_lastTickTime, nowTicks);
		}
	}
}
