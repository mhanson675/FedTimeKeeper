using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Utilities
{
    /// <summary>
    /// Calculates Federal Leave balances based on Accrual Rate and a provided FederalPayPeriod.
    /// </summary>
    public class FederalLeaveCalculator
    {
        /// <summary>
        /// The Annual Leave Accrual Rate, varies based on Time in Service.
        /// </summary>
        public int LeaveAccrualRate { get; private set; }
        
        /// <summary>
        /// The Sick Leave Accrual Rate, standard 4 hours per Pay Period.
        /// </summary>
        public int SickLeaveAccrualRate => 4;

        public FederalLeaveCalculator(int leaveAccrualRate)
        {
            LeaveAccrualRate = leaveAccrualRate;
        }

        public int EndingLeaveBalance(FederalPayPeriod period)
        {
            return period.Period * LeaveAccrualRate;
        }

        public int EndingSickLeaveBalance(FederalPayPeriod period)
        {
            return period.Period * SickLeaveAccrualRate;
        }

        public (int Annual, int Sick) EndingBalances(FederalPayPeriod period)
        {
            return (EndingLeaveBalance(period), EndingSickLeaveBalance(period));
        }
    }
}
