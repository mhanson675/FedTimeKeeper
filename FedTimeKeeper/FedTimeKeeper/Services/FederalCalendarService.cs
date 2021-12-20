using System;
using System.Collections.Generic;
using System.Linq;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;

namespace FedTimeKeeper.Services
{
    public class FederalCalendarService : IFederalCalendarService
    {
        private readonly List<ICalendar> payCalendars;

        /// <summary>
        /// A readonly list of all the ICalendars contained within the service.
        /// </summary>
        public IReadOnlyList<ICalendar> PayCalendars => payCalendars.AsReadOnly();

        public FederalCalendarService()
        {
            //TODO: Remove HardCoding
            payCalendars = new List<ICalendar>
            {
                new FederalPayCalendar(DateTime.Parse("2022-01-02")),
                new FederalPayCalendar(DateTime.Parse("2023-01-01")),
                new FederalPayCalendar(DateTime.Parse("2024-01-14")),
                new FederalPayCalendar(DateTime.Parse("2025-01-12")),
                new FederalPayCalendar(DateTime.Parse("2026-01-11")),
            };
        }

        /// <summary>
        /// Gets the ICalendar instance that contains the given date.
        /// </summary>
        /// <param name="date">The date to get the ICalendar for.</param>
        /// <param name="payCalendar">Contains the ICalendar that contains the given date, if the date is found; otherwise null</param>
        /// <returns>True if the date falls within the Calendar; otherwise false.</returns>
        public bool TryGetPayCalendarForDate(DateTime date, out ICalendar payCalendar)
        {
            payCalendar = payCalendars.FirstOrDefault(c => c.IncludesDate(date));
            return payCalendar != null;
        }

        /// <summary>
        /// Gets the FederalPayPeriod instance that contains the given date.
        /// </summary>
        /// <param name="date">The date to get the FederalPayPeriod for.</param>
        /// <param name="payPeriod">Contains the <see cref="FederalPayPeriod"/> that contains the given date, if one is found; otherwise null</param>
        /// <returns>True if the date falls within any of the <see cref="ICalendar"/>s; otherwise false.</returns>
        public bool TryGetPayPeriodForDate(DateTime date, out FederalPayPeriod payPeriod)
        {
            if (!TryGetPayCalendarForDate(date, out ICalendar calendar))
            {
                payPeriod = null;
                return false;
            }

            return calendar.TryGetPayPeriodForDate(date, out payPeriod);
        }

        /// <summary>
        /// Gets the previous FederalPayPeriod instance associated with the given date.
        /// </summary>
        /// <param name="date">The date to get the previous FederalPayPeriod for.</param>
        /// <param name="payPeriod">Contains the previous <see cref="FederalPayPeriod"/> associated with given date, if one is found; otherwise null</param>
        /// <returns>True if the date falls within any of the <see cref="ICalendar"/>s; otherwise false.</returns>
        public bool TryGetPreviousPayPeriod(DateTime date, out FederalPayPeriod payPeriod)
        {
            if (!TryGetPayCalendarForDate(date, out ICalendar calendar))
            {
                payPeriod = null;
                return false;
            }

            if (calendar.TryGetPreviousPayPeriod(date, out payPeriod))
            {
                return true;
            }

            DateTime previousPeriodEndDate = calendar.StartDate.AddDays(-1);

            if (!TryGetPayCalendarForDate(previousPeriodEndDate, out calendar))
            {
                payPeriod = null;
                return false;
            }

            return calendar.TryGetPayPeriodForDate(previousPeriodEndDate, out payPeriod);
        }
    }
}