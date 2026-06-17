using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniIT.Utils
{
	public class MainThreadRunner : IMainThreadRunner
	{
		private readonly int _mainThreadId;
		private readonly SynchronizationContext _synchronizationContext;
		private readonly TaskScheduler _scheduler;

		public MainThreadRunner()
		{
			_mainThreadId = Thread.CurrentThread.ManagedThreadId;
			_synchronizationContext = SynchronizationContext.Current;
			_scheduler = _synchronizationContext != null ?
				TaskScheduler.FromCurrentSynchronizationContext() :
				TaskScheduler.Current;
		}

		public void RunInMainThread(Action action)
		{
			if (Thread.CurrentThread.ManagedThreadId == _mainThreadId)
			{
				action();
			}
			else if (_synchronizationContext != null)
			{
				_synchronizationContext.Post(RunAction, action);
			}
			else
			{
				_ = Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.DenyChildAttach, _scheduler);
			}
		}

		private static void RunAction(object state)
		{
			((Action)state).Invoke();
		}
	}
}
