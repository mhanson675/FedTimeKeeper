using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedTimeKeeper.Utilities.Tests.HelperExtensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetLastSunday(this DateTime date) => date.DayOfWeek switch
        {
            DayOfWeek.Sunday => date.Date.AddDays(-7),
            DayOfWeek.Monday => date.Date.AddDays(-1),
            DayOfWeek.Tuesday => date.Date.AddDays(-2),
            DayOfWeek.Wednesday => date.Date.AddDays(-3),
            DayOfWeek.Thursday => date.Date.AddDays(-4),
            DayOfWeek.Friday => date.Date.AddDays(-5),
            DayOfWeek.Saturday => date.Date.AddDays(-6),
            _ => throw new ArgumentOutOfRangeException()
        };

        public static DateTime GetFirstSundayOfYear(this DateTime date)
        {
            var first = new DateTime(date.Year, 01, 01);

            return first.DayOfWeek switch
            {
                DayOfWeek.Sunday => first,
                DayOfWeek.Monday => first.Date.AddDays(6),
                DayOfWeek.Tuesday => first.Date.AddDays(5),
                DayOfWeek.Wednesday => first.Date.AddDays(4),
                DayOfWeek.Thursday => first.Date.AddDays(3),
                DayOfWeek.Friday => first.Date.AddDays(2),
                DayOfWeek.Saturday => first.Date.AddDays(1),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
