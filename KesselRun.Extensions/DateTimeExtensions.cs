using System;

namespace KesselRun.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsOlderThanDays(this DateTime source, double numberOfDays)
        {
            return source.AddDays(numberOfDays) < DateTime.Now;
        }
        
        public static bool IsOlderThanHours(this DateTime source, double numberOfHours)
        {
            return source.AddHours(numberOfHours) < DateTime.Now;
        }

        public static bool IsOlderThanMonths(this DateTime source, int numberOfMonths)
        {
            return source.AddMonths(numberOfMonths) < DateTime.Now;
        }

        public static bool IsOlderThanWeeks(this DateTime source, int numberOfWeeks)
        {
            var numberOfWeeksOld = DateTime.Now.Subtract(source).Days / 7;
            return numberOfWeeks > numberOfWeeksOld;
        }

        public static bool IsOlderThanYears(this DateTime source, int numberOfYears)
        {
            return source.AddYears(numberOfYears) < DateTime.Now;
        }

        public static bool Between(this DateTime source, DateTime rangeStart, DateTime rangeEnd)
        {
            return source.Ticks >= rangeStart.Ticks && source.Ticks <= rangeEnd.Ticks;
        }

        /// <summary>
        /// Taken from http://www.danylkoweb.com/Blog/10-extremely-useful-net-extension-methods-8J
        /// My thanks to the author.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToReadableTime(this DateTime source)
        {
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - source.Ticks);

            double delta = ts.TotalSeconds;
            if (delta < 60)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 120)
            {
                return "a minute ago";
            }
            if (delta < 2700) // 45 * 60
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 5400) // 90 * 60
            {
                return "an hour ago";
            }
            if (delta < 86400) // 24 * 60 * 60
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 172800) // 48 * 60 * 60
            {
                return "yesterday";
            }
            if (delta < 2592000) // 30 * 24 * 60 * 60
            {
                return ts.Days + " days ago";
            }
            if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            var years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }

        public static bool IsWorkingDay(this DateTime date)
        {
            return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
        }
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }
        public static DateTime GetNextWorkDay(this DateTime date)
        {
            var nextDay = date;

            while (!nextDay.IsWorkingDay())
            {
                nextDay = nextDay.AddDays(1);
            }

            return nextDay;
        }

        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - current.DayOfWeek;

            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }

            DateTime result = current.AddDays(offsetDays);

            return result;
        }
    }
}
