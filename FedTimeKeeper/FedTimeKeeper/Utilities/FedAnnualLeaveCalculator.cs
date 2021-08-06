using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedTimeKeeper.Utilities
{
    public class FedAnnualLeaveCalculator
    {
        private int accrualRate;

        public FedAnnualLeaveCalculator(int accrualRate = 8)
        {
            this.accrualRate = accrualRate;
        }

        public double PayPeriodEndingLeaveBalance(FederalPayPeriod payPeriod)
        {
            return payPeriod.Period * accrualRate;
        }
    }
}
