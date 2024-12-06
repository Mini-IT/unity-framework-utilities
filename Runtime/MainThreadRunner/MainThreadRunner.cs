using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniIT.Utils
{
	public class MainThreadRunner : IMainThreadRunner
	{
		private readonly TaskScheduler _mainThreadScheduler;

		public MainThreadRunner()
		{
			_mainThreadScheduler = SynchronizationContext.Current != null ?
				TaskScheduler.FromCurrentSynchronizationContext() :
				TaskScheduler.Current;
		}

		public void RunInMainThread(Action action)
		{
			new Task(action).RunSynchronously(_mainThreadScheduler);
		}
	}
}
