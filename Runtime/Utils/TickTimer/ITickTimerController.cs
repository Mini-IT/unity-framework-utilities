using System;

namespace MiniIT.Utils
{
	public interface ITickTimerController : IDisposable
	{
		ITickTimer CreateTimer(TimeSpan interval);
		void DestroyTimer(ITickTimer timer);
	}
}
