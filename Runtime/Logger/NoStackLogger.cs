#if MINIIT_LOGGER

using MiniIT.Logging;
using MiniIT.Logging.Unity;
using UnityEngine;

namespace MiniIT.Framework.Logging
{
	public class NoStackLogger : ISimpleLogger
	{
		private readonly Microsoft.Extensions.Logging.ILogger _logger;

		public NoStackLogger(string serviceName)
		{
			_logger = UnityLoggerFactory.Default.CreateLogger(serviceName);
		}

		public void Log(string text)
		{
			var logtype = Application.GetStackTraceLogType(LogType.Log);
			Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
			_logger.Log(text);
			Application.SetStackTraceLogType(LogType.Log, logtype);
		}
	}
}

#endif
