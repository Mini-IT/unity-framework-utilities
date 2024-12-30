using System;
using UnityEngine;

namespace MiniIT.Unity
{
	public class FrameRateDrivenTimer : FrameRateDrivenStopwatch
	{
		public bool AutoReset { get; set; }
		private Action TimerCallback { get; set; }

		public double Interval
		{
			get => _interval;
			set => _interval = value > 0.0 ? value : throw new ArgumentException("Timer interval cannot be zero.");
		}

		private double _interval;

		public FrameRateDrivenTimer(Action timerCallback)
		{
			TimerCallback = timerCallback;
			IsRunning = false;
			AutoReset = true;
			_interval = 100.0;
		}

		public override void Start() // Start the timer always reset
		{
			Elapsed = TimeSpan.Zero;
			IsRunning = true;
		}

		public override void OnLoopSystemUpdate()
		{
			base.OnLoopSystemUpdate();

			if (!IsRunning)
			{
				return;
			}

			if (Elapsed.TotalMilliseconds >= _interval)
			{
				if (AutoReset)
				{
					Restart();
				}
				else
				{
					Reset();
				}

				TimerCallback?.Invoke();
			}
		}
	}
}
