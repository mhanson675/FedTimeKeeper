namespace FedTimeKeeper.Utilities
{
    public interface IFederalLeaveCalculator
    {
        /// <summary>
        /// Returns the unadjusted ending leave balance based on accrual rate and the given pay period.
        /// </summary>
        /// <param name="period">The pay period to use for calculating the leave balance</param>
        /// <returns>The Leave Balance</returns>
        int EndingLeaveBalance(FederalPayPeriod period);

        /// <summary>
        /// Returns the unadjusted ending sick leave balance based on accrual rate and the given pay period.
        /// </summary>
        /// <param name="period">The pay period to use for calculating the sick leave balance</param>
        /// <returns>The Sick Leave Balance</returns>
        int EndingSickLeaveBalance(FederalPayPeriod period);

        /// <summary>
        /// Returns both the ending Annual Leave and Sick Leave balance for the given pay period, as a (int, int) tuple.
        /// </summary>
        /// <param name="period">The pay period to user for calculating the leave balances.</param>
        /// <returns>A (int, int) tuple, with the Annual Leave as the first int, and the Sick Leave as the last int.</returns>
        (int Annual, int Sick) EndingBalances(FederalPayPeriod period);
    }
}