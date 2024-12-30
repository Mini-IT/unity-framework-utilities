using System;
using UnityEngine;
using UnityEngine.LowLevel;

namespace MiniIT.Unity
{
	public class FrameRateDrivenStopwatch : IDisposable
	{
		public TimeSpan Elapsed { get; set; }
		public bool IsRunning { get; set; }

		private PlayerLoopSystem _loopSystem;

		public FrameRateDrivenStopwatch()
		{
			InitLoopSystem();
			IsRunning = true;
		}

		public void Reset()
		{
			Elapsed = TimeSpan.Zero;
			IsRunning = false;
		}

		public void Restart()
		{
			Elapsed = TimeSpan.Zero;
			Start();
		}

		public virtual void Start()
		{
			IsRunning = true;
		}

		public void Stop()
		{
			IsRunning = false;
		}

		public void Dispose()
		{
			if (_loopSystem.updateDelegate != null)
			{
				RemoveLoopSystem();
			}
		}

		protected void InitLoopSystem()
		{
			_loopSystem = new PlayerLoopSystem()
			{
				updateDelegate = OnLoopSystemUpdate,
				type = typeof(FrameRateDrivenStopwatch)
			};

			UnityPlayerLoopSystemUtility.AddLoopSystem(_loopSystem);
		}

		private void RemoveLoopSystem()
		{
			UnityPlayerLoopSystemUtility.RemoveLoopSystem(_loopSystem);
			_loopSystem = default;
		}

		public virtual void OnLoopSystemUpdate()
		{
			if (!IsRunning)
			{
				return;
			}

			int targetFrameRate = Application.targetFrameRate;
			double frameTime = (targetFrameRate > 0) ? 1.0 / targetFrameRate : Time.unscaledDeltaTime;
			Elapsed = Elapsed.Add(TimeSpan.FromSeconds(frameTime));
		}
	}
}
