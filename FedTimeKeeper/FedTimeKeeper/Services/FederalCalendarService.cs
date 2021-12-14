using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FedTimeKeeper.Utilities;

namespace FedTimeKeeper.Services
{
    public class FederalCalendarService
    {
        private readonly List<FederalPayCalendar> payCalendars;

        public IReadOnlyList<FederalPayCalendar> PayCalendars => payCalendars.AsReadOnly();

        public FederalCalendarService()
        {
            payCalendars = new List<FederalPayCalendar>();
        }

        public bool TryGetPayCalendarForDate(DateTime date, out FederalPayCalendar payCalendar)
        {
            payCalendar = payCalendars.FirstOrDefault(c => c.IncludesDate(date));
            return payCalendar != null;
        }
    }
}
