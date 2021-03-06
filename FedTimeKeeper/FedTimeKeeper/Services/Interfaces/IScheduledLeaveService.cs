using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    /// <summary>
    /// CRUD operations for Scheduled Leave entries in the local database instance.
    /// </summary>
    public interface IScheduledLeaveService
    {
        /// <summary>
        /// Get the number of leave hours taken for the given type, as of the given date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="type">The type of leave taken.</param>
        /// <returns>The number of hours taken between the start date and end date.</returns>
        double GetHoursTaken(DateTime startDate, DateTime endDate, LeaveType type);
        /// <summary>
        /// Gets all the ScheduledLeave entries
        /// </summary>
        /// <returns>A list of ScheduledLeave entries</returns>
        IEnumerable<ScheduledLeave> GetAllScheduled();

        /// <summary>
        /// Gets all of the ScheduledLeave entries from the database who's EndDates have already passed based on the provided date.
        /// </summary>
        /// <param name="date">The date to check against</param>
        /// <returns>A list of ScheduledLeave entries who's EndDates occur prior to the given date</returns>
        IEnumerable<ScheduledLeave> GetPastScheduled(DateTime date);

        /// <summary>
        /// Deletes the ScheduledLeave entry from the database if it exists.
        /// </summary>
        /// <param name="leave">The ScheduleLeave entry to delete.</param>
        void DeleteLeave(ScheduledLeave leave);
        
        /// <summary>
        /// Saves the ScheduledLeave instance.
        /// </summary>
        /// <param name="leave">The ScheduledLeave entry to save</param>
        void SaveLeave(ScheduledLeave leave);
    }
}
