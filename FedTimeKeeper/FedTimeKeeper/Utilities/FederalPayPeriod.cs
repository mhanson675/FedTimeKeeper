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

        public FederalPayPeriod(int payPeriod)
        {
            if (payPeriod < 1 || payPeriod > 26)
            {
                throw new ArgumentOutOfRangeException(nameof(payPeriod));
            }
            Period = payPeriod;
        }

        public override string ToString()
        {
            return Period.ToString();
        }
    }
}
