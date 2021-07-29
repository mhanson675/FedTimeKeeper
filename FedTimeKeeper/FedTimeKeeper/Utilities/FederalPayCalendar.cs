using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedTimeKeeper.Utilities
{
    public class FederalPayCalendar
    {
        private int payPeriodLength = 14;
        public DateTime WeekOneStartDate { get; }
        public FederalPayCalendar(DateTime weekOneStartDate)
        {
            WeekOneStartDate = weekOneStartDate;
        }

        public DateTime GetPayPeriodStartDate(FederalPayPeriod payPeriod)
        {
            return WeekOneStartDate.AddDays(payPeriodLength * (payPeriod.Period - 1));
        }

        public DateTime GetPayPeriodEndDate(FederalPayPeriod payPeriod)
        {
            return WeekOneStartDate.AddDays(payPeriodLength * payPeriod.Period - 1);
        }

        public FederalPayPeriod GetCurrentPayPeriod(DateTime date)
        {
            var difference = date.Subtract(WeekOneStartDate).Days;

            var period = difference / payPeriodLength + 1;

            return new FederalPayPeriod(period);
        }

    }
}
