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
        private int startingBalance;

        public FedAnnualLeaveCalculator(int startingBalance = 0, int accrualRate = 8)
        {
            this.startingBalance = startingBalance;
            this.accrualRate = accrualRate;
        }
        public int PayPeriodEndingLeaveBalance(FederalPayPeriod payPeriod)
        {
            return payPeriod.Period * accrualRate + startingBalance;
        }
    }
}
