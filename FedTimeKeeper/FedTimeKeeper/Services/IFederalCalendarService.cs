using System;
using FedTimeKeeper.Utilities;

namespace FedTimeKeeper.Services
{
    public interface IFederalCalendarService
    {
        bool TryGetPayCalendarForDate(DateTime date, out ICalendar payCalendar);
        bool TryGetPayPeriodForDate(DateTime date, out FederalPayPeriod payPeriod);
        bool TryGetPreviousPayPeriod(DateTime date, out FederalPayPeriod payPeriod);
    }
}