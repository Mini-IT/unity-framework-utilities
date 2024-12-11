using UnityEngine;

namespace MiniIT.Framework.Logging
{
	public class DefaultNoStackLogger : ISimpleLogger
	{
		private readonly string _serviceName;

		public DefaultNoStackLogger(string serviceName)
		{
			_serviceName = serviceName;
		}

		public void Log(string text)
		{
			var logtype = Application.GetStackTraceLogType(LogType.Log);
			Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
			Debug.Log($"[{_serviceName}] {text}");
			Application.SetStackTraceLogType(LogType.Log, logtype);
		}
	}
}

