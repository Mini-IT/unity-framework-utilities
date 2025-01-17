using System;

namespace MiniIT.Unity
{
	public class FrameRateDrivenTimer : FrameRateDrivenStopwatch
	{
		public bool AutoReset { get; set; }
		public TimeSpan Interval{ get; set; }

		private readonly Action _timerCallback;

		public FrameRateDrivenTimer(Action timerCallback)
			: this(TimeSpan.FromSeconds(1), true, timerCallback)
		{
		}

		public FrameRateDrivenTimer(TimeSpan interval, bool autoReset, Action timerCallback)
			: base(false)
		{
			Interval = interval;
			AutoReset = autoReset;
			_timerCallback = timerCallback;
		}

		public override void Start()
		{
			if (IsRunning)
			{
				return;
			}

			Reset();
			base.Start();
		}

		protected override void OnLoopSystemUpdate()
		{
			if (!IsRunning)
			{
				return;
			}

			base.OnLoopSystemUpdate();

			if (Elapsed < Interval)
			{
				return;
			}

			if (AutoReset)
			{
				Restart();
			}
			else
			{
				Reset();
			}

			_timerCallback?.Invoke();
		}
	}
}
