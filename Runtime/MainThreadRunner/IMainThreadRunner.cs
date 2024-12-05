using System;

namespace MiniIT.Utils
{
	public interface IMainThreadRunner
	{
		void RunInMainThread(Action action);
	}
}
