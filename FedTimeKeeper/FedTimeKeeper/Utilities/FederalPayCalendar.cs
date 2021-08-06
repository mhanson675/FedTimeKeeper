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
        private DateTime firstpayPeriodStartDate;
        private DateTime lastPayPeriodStartDate;
        private int payPeriodCount;

        public FederalPayCalendar(DateTime firstPayPeriod, DateTime lastPayPeriod)
        {
            firstpayPeriodStartDate = firstPayPeriod;
            lastPayPeriodStartDate = lastPayPeriod;
            GetPayPeriodCount(firstPayPeriod, lastPayPeriod);
        }


        public DateTime GetPayPeriodStartDate(DateTime date)
        {
            var difference = date.Subtract(firstpayPeriodStartDate).Days;
            var currentPayPeriod = difference / payPeriodLength;

            var currentPayPeriodStart = firstpayPeriodStartDate.AddDays(currentPayPeriod * payPeriodLength);

            return currentPayPeriodStart;
        }

        public DateTime GetPayPeriodEndDate(DateTime date)
        {
            var payPeriodStart = GetPayPeriodStartDate(date);
            return payPeriodStart.AddDays(payPeriodLength - 1);
        }
        
        public FederalPayPeriod GetCurrentPayPeriod(DateTime date)
        {
            var difference = date.Subtract(firstpayPeriodStartDate).Days;

            var start = GetPayPeriodStartDate(date);
            var end = GetPayPeriodEndDate(date);
            var period = difference / payPeriodLength + 1;

            return new FederalPayPeriod(start, end, period);
        }

        public FederalPayPeriod GetPreviousPayPeriod(DateTime date)
        {
            var current = GetCurrentPayPeriod(date);

            var oneWeek = new TimeSpan(payPeriodLength, 0, 0, 0);
            var previousStart = current.StartDate.Subtract(oneWeek);
            var previousEnd = current.EndDate.Subtract(oneWeek);
            var previousPeriod = current.Period - 1;

            return new FederalPayPeriod(previousStart, previousEnd, previousPeriod);
        }

        public FederalPayPeriod GetNextPayPeriod(DateTime date)
        {
            var current = GetCurrentPayPeriod(date);

            var nextStart = current.StartDate.AddDays(payPeriodLength);
            var nextEnd = current.EndDate.AddDays(payPeriodLength);
            var nextPeriod = current.Period + 1;

            return new FederalPayPeriod(nextStart, nextEnd, nextPeriod);
        }

        public FederalPayPeriod GetFirstPayPeriod()
        {
            var start = firstpayPeriodStartDate;
            var end = start.AddDays(payPeriodLength - 1);
            var period = 1;

            return new FederalPayPeriod(start, end, period);
        }

        public FederalPayPeriod GetFinalPayPeriod()
        {
            var start = lastPayPeriodStartDate;
            var end = start.AddDays(payPeriodLength - 1);

            return new FederalPayPeriod(start, end, payPeriodCount);
        }

        private void GetPayPeriodCount(DateTime firstPayPeriod, DateTime lastPayPeriod)
        {
            payPeriodCount = (lastPayPeriod.Subtract(firstPayPeriod).Days / payPeriodLength) + 1;
        }
    }
}