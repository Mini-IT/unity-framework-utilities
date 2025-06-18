using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniIT.Utils
{
	public class MainThreadRunner : IMainThreadRunner
	{
		private readonly TaskScheduler _scheduler;

		public MainThreadRunner()
		{
			_scheduler = SynchronizationContext.Current != null ?
				TaskScheduler.FromCurrentSynchronizationContext() :
				TaskScheduler.Current;
		}

		public void RunInMainThread(Action action)
		{
			if (TaskScheduler.Current == _scheduler)
			{
				action();
			}
			else
			{
				_ = Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.DenyChildAttach, _scheduler);
			}
		}
	}
}
