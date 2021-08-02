using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedTimeKeeper.Utilities
{
    public class FederalPayPeriod
    {
        public int Period { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public FederalPayPeriod(DateTime startDate, DateTime endDate, int payPeriod)
        {
            if (endDate.Subtract(startDate).Days != 13)
            {
                throw new ArgumentOutOfRangeException(nameof(endDate));
            }
            StartDate = startDate;
            EndDate = endDate;

            if (payPeriod < 1 || payPeriod > 26)
            {
                throw new ArgumentOutOfRangeException(nameof(payPeriod));
            }
            Period = payPeriod;
        }

        public override string ToString()
        {
            var start = StartDate.ToString("dd-MMM");
            var end = EndDate.ToString("dd-MMM-yy");
            var period = Period.ToString();

            return $"Pay Period: {period} - {start} to {end}";
        }
    }
}