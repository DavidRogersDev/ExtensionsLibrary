using System;

namespace KesselRun.Extensions
{
    public static class Int32Extensions
    {
        public static TimeSpan Days(this int number)
        {
            return new TimeSpan(number, 0, 0, 0);
        }

        public static TimeSpan Minutes(this int number)
        {
            return new TimeSpan(0, 0, number, 0);
        }

        public static TimeSpan Hours(this int number)
        {
            return new TimeSpan(0, number, 0, 0);
        }

        public static TimeSpan Seconds(this int number)
        {
            return new TimeSpan(0, 0, 0, number);
        }
    }
}
