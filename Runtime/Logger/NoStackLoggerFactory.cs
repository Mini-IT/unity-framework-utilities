
namespace MiniIT.Framework.Logging
{
	public static class NoStackLoggerFactory
	{
		public static ISimpleLogger CreateLogger(string serviceName)
		{
#if MINIIT_LOGGER
			return new NoStackLogger(serviceName);
#else
			return new DefaultNoStackLogger(serviceName);
#endif
		}
	}
}

