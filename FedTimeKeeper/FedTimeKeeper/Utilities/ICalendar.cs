using System;
using System.Collections.Generic;

namespace FedTimeKeeper.Utilities
{
    public interface ICalendar
    {
        /// <summary>
        /// A readonly list of the Pay Periods within the Pay Calendar
        /// </summary>
        IReadOnlyList<FederalPayPeriod> PayPeriods { get; }

        /// <summary>
        /// The first date of the Pay Calendar
        /// </summary>
        DateTime StartDate { get; }

        /// <summary>
        /// The last date of the Pay Calendar
        /// </summary>
        DateTime EndDate { get; }

        /// <summary>
        /// The year of the Pay Calendar
        /// </summary>
        int PayYear { get; }

        /// <summary>
        /// The number of Pay Periods in the Pay Calendar
        /// </summary>
        int NumberOfPayPeriods { get; }

        /// <summary>
        /// Gets the first Pay Period of the Pay Year
        /// </summary>
        /// <returns>The first Pay Period</returns>
        FederalPayPeriod GetFirstPayPeriod();

        /// <summary>
        /// Gets the final Pay Period of the Pay Year
        /// </summary>
        /// <returns>The final Pay Period</returns>
        FederalPayPeriod GetFinalPayPeriod();

        /// <summary>
        /// Gets the Pay Period associated with the given date.
        /// </summary>
        /// <param name="date">The date to get the Pay Period for.</param>
        /// <param name="payPeriod">Contains the Pay Period associated with the given date, if the date is found; otherwise null</param>
        /// <returns>True if the date falls within the Pay Calendar; otherwise false.</returns>
        bool TryGetPayPeriodForDate(DateTime date, out FederalPayPeriod payPeriod);

        /// <summary>
        /// Gets the Previous Pay Period associated with the given date.
        /// </summary>
        /// <param name="date">The date to get the Pay Period for.</param>
        /// <param name="payPeriod">Contains the Previous Pay Period associated with the given date, if the date is found; otherwise null</param>
        /// <returns>True if both the date and the Previous Pay Period fall within the Pay Calendar; otherwise false.</returns>
        bool TryGetPreviousPayPeriod(DateTime date, out FederalPayPeriod payPeriod);

        /// <summary>
        /// Checks if the given date falls within the Pay Periods for the Pay Calendar.
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>True if the given date falls within the Pay Periods of the Pay Calendar; otherwise false.</returns>
        bool IncludesDate(DateTime date);
    }
}