using System;
using System.Collections.Generic;
using UnityEngine.LowLevel;

namespace MiniIT.Utils
{
	public class TickTimerController : ITickTimerController
	{
		private List<TickTimer> _timers;
		private PlayerLoopSystem _loopSystem;

		public ITickTimer CreateTimer(TimeSpan interval)
		{
			var timer = new TickTimer(interval);
			AddTimer(timer);
			return timer;
		}

		public void DestroyTimer(ITickTimer timer)
		{
			_timers?.Remove(timer as TickTimer);
		}

		private void AddTimer(TickTimer timer)
		{
			if (_timers == null)
			{
				Init();
			}

			_timers?.Add(timer);
		}

		public void Dispose()
		{
			if (_loopSystem.updateDelegate != null)
			{
				RemoveLoopSystem();
			}

			_timers = null;
		}

		private void Init()
		{
			_timers = new();
			InitLoopSystem();
		}

		private void InitLoopSystem()
		{
			PlayerLoopSystem loopSystem = PlayerLoop.GetCurrentPlayerLoop();

			_loopSystem = new PlayerLoopSystem()
			{
				updateDelegate = CustomUpdate,
				type = typeof(TickTimer)
			};

			Array.Resize(ref loopSystem.subSystemList, loopSystem.subSystemList.Length + 1);
			loopSystem.subSystemList[^1] = _loopSystem;

			PlayerLoop.SetPlayerLoop(loopSystem);
		}

		private void RemoveLoopSystem()
		{
			PlayerLoopSystem loopSystem = PlayerLoop.GetCurrentPlayerLoop();

			int index = Array.IndexOf(loopSystem.subSystemList, _loopSystem);
			if (index >= 0)
			{
				var list = new PlayerLoopSystem[loopSystem.subSystemList.Length - 1];
				Array.ConstrainedCopy(loopSystem.subSystemList, 0, list, 0, index);
				Array.ConstrainedCopy(loopSystem.subSystemList, index + 1, list, index, loopSystem.subSystemList.Length - index - 1);
				loopSystem.subSystemList = list;

				PlayerLoop.SetPlayerLoop(loopSystem);
			}

			_loopSystem = default;
		}

		private void CustomUpdate()
		{
			DateTime now = DateTime.UtcNow;

			for (int i = 0; _timers != null && i < _timers.Count; i++)
			{
				var timer = _timers[i];
				if (timer == null)
				{
					_timers.RemoveAt(i);
					i--;
					continue;
				}

				if (timer.IsRunning && timer.GetTimeFromLastTick(now) >= timer.Interval)
				{
					timer.InvokeTick(now);
				}
			}
		}
	}
}
