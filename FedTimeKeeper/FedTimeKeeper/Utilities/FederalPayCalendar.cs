﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FedTimeKeeper.Utilities
{
    /// <summary>
    /// <para>
    /// An object modeling a standard Federal Pay Calendar Year, which generally consists of 26 Pay Periods.
    /// Each Pay Period begins on a Sunday and ends on a Saturday, covering a total of 14 days.
    /// A Pay Year typically has 26 Pay periods, but may have 27 periods in years where the previous pay year ends on Saturday, December 31st.
    /// </para>
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
        public int PayYear => StartDate.Year;

        /// <summary>
        /// A readonly list of the Pay Periods within the Pay Calendar
        /// </summary>
        public IReadOnlyList<FederalPayPeriod> PayPeriods => payPeriods.AsReadOnly();

        /// <summary>
        /// The number of Pay Periods in the Pay Calendar
        /// </summary>
        public int NumberOfPayPeriods => PayPeriods.Count();


        /// <summary>
        /// Constructs a new Pay Calendar using the given StartDate.
        /// </summary>
        /// <param name="startDate">The starting date for the Pay Calendar</param>
        public FederalPayCalendar(DateTime startDate)
        {
            if (startDate.DayOfWeek != DayOfWeek.Sunday)
            {
                throw new ArgumentOutOfRangeException(nameof(startDate), startDate.DayOfWeek, "The start date must be a Sunday.");
            }

            if (startDate.Month != 1)
            {
                throw new ArgumentOutOfRangeException(nameof(startDate), startDate.Month, "The start date must be in January (01).");
            }

            StartDate = startDate;
            payPeriods = new List<FederalPayPeriod>();

            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (startDate.DayOfYear == 1)
            {
                BuildCalendar(startDate, 27);
            }
            else
            {
                BuildCalendar(startDate, 26);
            }
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
            if (!IncludesDate(date))
            {
                payPeriod = null;
                return false;
            }

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
            if (!IncludesDate(date))
            {
                payPeriod = null;
                return false;
            }

            FederalPayPeriod currentPayPeriod = PayPeriods.FirstOrDefault(p => p.IncludesDate(date));

            if (currentPayPeriod is null || currentPayPeriod.Period == 1)
            {
                payPeriod = null;
                return false;
            }

            payPeriod = PayPeriods.FirstOrDefault(p => p.Period == (currentPayPeriod.Period - 1));

            return payPeriod != null;
        }

        /// <summary>
        /// Checks if the given date falls within the Pay Periods for the Pay Calendar.
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>True if the given date falls within the Pay Periods of the Pay Calendar; otherwise false.</returns>
        public bool IncludesDate(DateTime date) => date.Date >= StartDate.Date && date.Date <= EndDate.Date;


        /// <summary>
        /// Builds the current instance using the given start date and pay period count.
        /// Populates the PayPeriods list and the EndDate property.
        /// </summary>
        /// <param name="startDate">The start date to use.</param>
        /// <param name="payPeriodsCount">The number of pay periods to build.</param>
        private void BuildCalendar(DateTime startDate, int payPeriodsCount)
        {
            DateTime periodStart = startDate;
            int payPeriod = 1;
            while (payPeriod <= payPeriodsCount)
            {
                FederalPayPeriod periodToAdd = new FederalPayPeriod(periodStart, payPeriod);

                payPeriods.Add(periodToAdd);

                periodStart = periodStart.AddDays(14);
                payPeriod++;
            }

            EndDate = periodStart.AddDays(-1);
        }
    }
}