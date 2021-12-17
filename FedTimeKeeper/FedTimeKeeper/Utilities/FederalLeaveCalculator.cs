using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Utilities
{
    /// <summary>
    /// Calculates Federal Leave balances based on Accrual Rate and a provided FederalPayPeriod.
    /// </summary>
    public class FederalLeaveCalculator : IFederalLeaveCalculator
    {
        /// <summary>
        /// The Annual Leave Accrual Rate, varies based on Time in Service.
        /// </summary>
        public int LeaveAccrualRate { get; private set; }
        
        /// <summary>
        /// The Sick Leave Accrual Rate, standard 4 hours per Pay Period.
        /// </summary>
        public int SickLeaveAccrualRate => 4;

        /// <summary>
        /// Creates an Instance of <see cref="FederalLeaveCalculator"/> with the given Annual Leave Accrual Rate.
        /// </summary>
        /// <param name="leaveAccrualRate">The Annual Leave Accrual Rate per pay period.</param>
        public FederalLeaveCalculator(int leaveAccrualRate)
        {
            LeaveAccrualRate = leaveAccrualRate;
        }

        /// <summary>
        /// Returns the unadjusted ending leave balance based on accrual rate and the given pay period.
        /// </summary>
        /// <param name="period">The pay period to use for calculating the leave balance</param>
        /// <returns>The Leave Balance</returns>
        public int EndingLeaveBalance(int period)
        {
            if (period is < 1 or > 27)
            {
                throw new ArgumentOutOfRangeException(nameof(period), period,
                    "Period must be greater than or equal to one, and less than 27");
            }

            return period * LeaveAccrualRate;
        }

        /// <summary>
        /// Returns the unadjusted ending sick leave balance based on accrual rate and the given pay period.
        /// </summary>
        /// <param name="period">The pay period to use for calculating the sick leave balance</param>
        /// <returns>The Sick Leave Balance</returns>
        public int EndingSickLeaveBalance(int period)
        {
            if (period is < 1 or > 27)
            {
                throw new ArgumentOutOfRangeException(nameof(period), period,
                    "Period must be greater than or equal to one, and less than 27");
            }

            return period * SickLeaveAccrualRate;
        }


        /// <summary>
        /// Returns both the ending Annual Leave and Sick Leave balance for the given pay period, as a (int, int) tuple.
        /// </summary>
        /// <param name="period">The pay period to user for calculating the leave balances.</param>
        /// <returns>A (int, int) tuple, with the Annual Leave as the first int, and the Sick Leave as the last int.</returns>
        public (int Annual, int Sick) EndingBalances(int period)
        {
            if (period is < 1 or > 27)
            {
                throw new ArgumentOutOfRangeException(nameof(period), period,
                    "Period must be greater than or equal to one, and less than 27");
            }

            return (EndingLeaveBalance(period), EndingSickLeaveBalance(period));
        }
    }
}
