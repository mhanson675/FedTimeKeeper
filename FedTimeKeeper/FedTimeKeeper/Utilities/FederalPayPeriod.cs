using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedTimeKeeper.Utilities
{
    /// <summary>
    /// An object modeling a standard Federal Pay Period, which consists of 14 consecutive days, beginning on a Sunday and ending on a Saturday.
    /// Each Pay Period also has a numerical identifier designating where it falls within it's Pay Calendar.
    /// </summary>
    public class FederalPayPeriod
    {
        private readonly int payPeriodLength = 14;
        private readonly DayOfWeek startDayOfWeek = DayOfWeek.Sunday;
        
        /// <summary>
        /// The sequence number for the Pay Period in the Pay Calendar Year
        /// </summary>
        public int Period { get; private set; }
        
        /// <summary>
        /// The start date of the Pay Period
        /// </summary>
        public DateTime StartDate { get; private set; }
        
        /// <summary>
        /// The end date of the Pay Period
        /// </summary>
        public DateTime EndDate => StartDate.AddDays(payPeriodLength-1);

        /// <summary>
        /// Creates an instance of a Pay Period with the given start date, designated as the given period of the Pay Calendar Year.
        /// </summary>
        /// <param name="startDate">The starting date</param>
        /// <param name="payPeriod">The period number</param>
        public FederalPayPeriod(DateTime startDate, int payPeriod)
        {
            if (startDate.DayOfWeek != startDayOfWeek)
            {
                throw new ArgumentOutOfRangeException(nameof(startDate), startDate.DayOfWeek, $"Start date must be a {DayOfWeek.Sunday}.");
            }
            
            StartDate = startDate;

            if (payPeriod is < 1 or > 27)
            {
                throw new ArgumentOutOfRangeException(nameof(payPeriod), payPeriod, "Pay Period must be greater than 1, and less than 27.");
            }
            Period = payPeriod;
        }

        /// <summary>
        /// Checks if the given date falls within the Pay Period
        /// </summary>
        /// <param name="dateToCheck">The date to check</param>
        /// <returns>True if the date falls within the Pay Period; otherwise false.</returns>
        public bool IncludesDate(DateTime dateToCheck)
        {
            return StartDate <= dateToCheck && EndDate >= dateToCheck;
        }

        /// <summary>
        /// Returns a reader friendly string representing the Pay Period
        /// </summary>
        /// <returns>A string representing the Pay Period</returns>
        public override string ToString()
        {
            string start = StartDate.ToString("dd-MMM-yy");
            string end = EndDate.ToString("dd-MMM-yy");
            string period = Period.ToString();

            return $"Pay Period: {period} - {start} to {end}";
        }
    }
}