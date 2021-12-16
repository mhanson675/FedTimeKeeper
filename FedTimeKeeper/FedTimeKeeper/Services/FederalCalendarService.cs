using System;
using System.Collections.Generic;
using System.Linq;
using FedTimeKeeper.Utilities;

namespace FedTimeKeeper.Services
{
    public class FederalCalendarService
    {
        private readonly List<FederalPayCalendar> payCalendars;

        public IReadOnlyList<FederalPayCalendar> PayCalendars => payCalendars.AsReadOnly();

        public FederalCalendarService()
        {
            //TODO: Remove HardCoding
            payCalendars = new List<FederalPayCalendar>
            {
                new FederalPayCalendar(DateTime.Parse("2022-01-02")),
                new FederalPayCalendar(DateTime.Parse("2023-01-01")),
                new FederalPayCalendar(DateTime.Parse("2024-01-14")),
                new FederalPayCalendar(DateTime.Parse("2025-01-12")),
                new FederalPayCalendar(DateTime.Parse("2026-01-11")),
            };
        }

        public bool TryGetPayCalendarForDate(DateTime date, out FederalPayCalendar payCalendar)
        {
            payCalendar = payCalendars.FirstOrDefault(c => c.IncludesDate(date));
            return payCalendar != null;
        }

        public bool TryGetPayPeriodForDate(DateTime date, out FederalPayPeriod payPeriod)
        {
            if (!TryGetPayCalendarForDate(date, out FederalPayCalendar calendar))
            {
                payPeriod = null;
                return false;
            }

            return calendar.TryGetPayPeriodForDate(date, out payPeriod);
        }

        public bool TryGetPreviousPayPeriod(DateTime date, out FederalPayPeriod payPeriod)
        {
            if (!TryGetPayCalendarForDate(date, out FederalPayCalendar calendar))
            {
                payPeriod = null;
                return false;
            }

            if (calendar.TryGetPreviousPayPeriod(date, out payPeriod))
            {
                return true;
            }

            DateTime previousPeriodEndDate = calendar.StartDate.AddDays(-1);

            if (TryGetPayCalendarForDate(previousPeriodEndDate, out calendar))
            {
                return calendar.TryGetPayPeriodForDate(previousPeriodEndDate, out payPeriod);
            }
            else
            {
                payPeriod = null;
                return false;
            }
        }
    }
}