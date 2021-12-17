using System;
using System.Collections.Generic;
using FedTimeKeeper.Utilities;

namespace FedTimeKeeper.Services.Interfaces
{
    /// <summary>
    /// An interface for managing ICalendar collections
    /// </summary>
    public interface IFederalCalendarService
    {
        /// <summary>
        /// A readonly list of all the ICalendars contained within the service.
        /// </summary>
        IReadOnlyList<ICalendar> PayCalendars { get; }
        /// <summary>
        /// Gets the ICalendar instance that contains the given date.
        /// </summary>
        /// <param name="date">The date to get the ICalendar for.</param>
        /// <param name="payCalendar">Contains the ICalendar that contains the given date, if the date is found; otherwise null</param>
        /// <returns>True if the date falls within the Calendar; otherwise false.</returns>
        bool TryGetPayCalendarForDate(DateTime date, out ICalendar payCalendar);
        /// <summary>
        /// Gets the FederalPayPeriod instance that contains the given date.
        /// </summary>
        /// <param name="date">The date to get the FederalPayPeriod for.</param>
        /// <param name="payPeriod">Contains the <see cref="FederalPayPeriod"/> that contains the given date, if one is found; otherwise null</param>
        /// <returns>True if the date falls within any of the <see cref="ICalendar"/>s; otherwise false.</returns>
        bool TryGetPayPeriodForDate(DateTime date, out FederalPayPeriod payPeriod);
        /// <summary>
        /// Gets the previous FederalPayPeriod instance associated with the given date.
        /// </summary>
        /// <param name="date">The date to get the previous FederalPayPeriod for.</param>
        /// <param name="payPeriod">Contains the previous <see cref="FederalPayPeriod"/> associated with given date, if one is found; otherwise null</param>
        /// <returns>True if the date falls within any of the <see cref="ICalendar"/>s; otherwise false.</returns>
        bool TryGetPreviousPayPeriod(DateTime date, out FederalPayPeriod payPeriod);

    }
}