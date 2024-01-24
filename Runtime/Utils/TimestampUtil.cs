using System;

namespace MiniIT.Utils
{
    public static class TimestampUtil
    {
        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static long GetTimestamp(DateTime datetime)
        {
            return (long)(datetime.Subtract(UnixEpoch).TotalSeconds);
        }

        public static DateTime UtcDateTimeFromTimestamp(long timestamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timestamp).UtcDateTime;
        }
    }
}