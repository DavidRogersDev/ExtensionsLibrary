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

    }
}
