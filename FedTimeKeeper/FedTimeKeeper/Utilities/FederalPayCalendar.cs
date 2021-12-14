using System;
using System.Collections.Generic;
using System.Linq;

namespace FedTimeKeeper.Utilities
{
    /// <summary>
    /// An object modeling a standard Federal Pay Calendar Year, which consists of roughly 26 Pay Periods, beginning on a Sunday and ending on a Saturday.
    /// The first Pay period may begin in December, and the last Pay Period may end in January depending on previously Pay Calendar Year.
    /// </summary>
    public class FederalPayCalendar
    {
        private readonly List<FederalPayPeriod> payPeriods;
        
        /// <summary>
        /// The first date of the Pay Calendar
        /// </summary>
        public DateTime StartDate { get; private set; }
        
        /// <summary>
        /// The last date of the Pay Calendar
        /// </summary>
        public DateTime EndDate { get; private set; }
        
        /// <summary>
        /// The year of the Pay Calendar
        /// </summary>
        public int PayYear => StartDate.Month == 1 ? StartDate.Year : StartDate.Year + 1;
        
        /// <summary>
        /// A readonly list of the Pay Periods within the Pay Calendar
        /// </summary>
        public IReadOnlyList<FederalPayPeriod> PayPeriods => payPeriods.AsReadOnly();
        
        /// <summary>
        /// The number of Pay Periods in the Pay Calendar
        /// </summary>
        public int NumberOfPayPeriods => PayPeriods.Count();

        /// <summary>
        /// Constructs a new Pay Calendar starting and ending on the dates provided.
        /// </summary>
        /// <param name="startDate">The first day of the Pay Calendar</param>
        /// <param name="endDate">The last day of the Pay Calendar</param>
        public FederalPayCalendar(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentOutOfRangeException(nameof(startDate), startDate, "The start date must be before the end date.");
            }

            if (startDate.DayOfWeek != DayOfWeek.Sunday)
            {
                throw new ArgumentOutOfRangeException(nameof(startDate), startDate.DayOfWeek, "The start date must be a Sunday.");
            }

            if (endDate.DayOfWeek != DayOfWeek.Saturday)
            {
                throw new ArgumentOutOfRangeException(nameof(endDate), endDate.DayOfWeek, "The end date must be a Saturday.");
            }

            StartDate = startDate;
            EndDate = endDate;
            payPeriods = PopulatePayPeriods(startDate, endDate);
        }

        private static List<FederalPayPeriod> PopulatePayPeriods(DateTime startDate, DateTime endDate)
        {
            List<FederalPayPeriod> periods = new List<FederalPayPeriod>();
            DateTime periodStart = startDate;
            int payPeriod = 1;
            while (periodStart < endDate)
            {
                FederalPayPeriod periodToAdd = new FederalPayPeriod(periodStart, payPeriod);

                periods.Add(periodToAdd);

                periodStart = periodStart.AddDays(14);
                payPeriod++;
            }

            return periods;
        }

        /// <summary>
        /// Gets the first Pay Period of the Pay Year
        /// </summary>
        /// <returns>The first Pay Period</returns>
        public FederalPayPeriod GetFirstPayPeriod() => PayPeriods.FirstOrDefault(p => p.Period == 1);
        
        /// <summary>
        /// Gets the final Pay Period of the Pay Year
        /// </summary>
        /// <returns>The final Pay Period</returns>
        public FederalPayPeriod GetFinalPayPeriod() => PayPeriods.FirstOrDefault(p => p.Period == NumberOfPayPeriods);
        
        /// <summary>
        /// Gets the Pay Period associated with the given date.
        /// </summary>
        /// <param name="date">The date to get the Pay Period for.</param>
        /// <param name="payPeriod">Contains the Pay Period associated with the given date, if the date is found; otherwise null</param>
        /// <returns>True if the date falls within the Pay Calendar; otherwise false.</returns>
        public bool TryGetPayPeriodForDate(DateTime date, out FederalPayPeriod payPeriod)
        {
            payPeriod = PayPeriods.FirstOrDefault(p => p.IncludesDate(date));
            return payPeriod != null;
        }

        /// <summary>
        /// Gets the Previous Pay Period associated with the given date.
        /// </summary>
        /// <param name="date">The date to get the Pay Period for.</param>
        /// <param name="payPeriod">Contains the Previous Pay Period associated with the given date, if the date is found; otherwise null</param>
        /// <returns>True if both the date and the Previous Pay Period fall within the Pay Calendar; otherwise false.</returns>
        public bool TryGetPreviousPayPeriod(DateTime date, out FederalPayPeriod payPeriod)
        {
            FederalPayPeriod currentPayPeriod = PayPeriods.FirstOrDefault(p => p.IncludesDate(date));
            
            if (currentPayPeriod is null)
            {
                payPeriod = null;
                return false;
            }

            DateTime lastPayPeriodEnd = currentPayPeriod.StartDate.Subtract(new TimeSpan(1));

            payPeriod = PayPeriods.FirstOrDefault(p => p.IncludesDate(lastPayPeriodEnd));
            return payPeriod != null;
        }
        
        /// <summary>
        /// Checks if the given date falls within the Pay Periods for the Pay Calendar.
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>True if the given date falls within the Pay Periods of the Pay Calendar; otherwise false.</returns>
        public bool IncludesDate(DateTime date) => PayPeriods.Any(p => p.IncludesDate(date));
    }
}