using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedTimeKeeper.Utilities
{
    public class FedSickLeaveCalculator
    {
        private int accrualRate = 4;

        public double PayPeriodEndingLeaveBalance(FederalPayPeriod payPeriod)
        {
            return payPeriod.Period * accrualRate;
        }
    }
}