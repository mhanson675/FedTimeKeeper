using FedTimeKeeper.Database;
using FedTimeKeeper.Models;
using FedTimeKeeper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FedTimeKeeper.Services
{
    /// <summary>
    /// CRUD operations for Scheduled Leave entries in the local database instance.
    /// </summary>
    public class ScheduledLeaveService : IScheduledLeaveService
    {
        private readonly ILocalDatabase database;


        public ScheduledLeaveService(ILocalDatabase database)
        {
            this.database = database;
        }

        /// <summary>
        /// Updates the ScheduledLeave instance if it exists; otherwise creates an entry and saves it.
        /// </summary>
        /// <param name="leave">The ScheduledLeave entry to save</param>
        public void SaveLeave(ScheduledLeave leave)
        {
            _ = leave?.Id == 0 ? database.AddLeave(leave) : database.UpdateLeave(leave);
        }

        /// <summary>
        /// Gets all the ScheduledLeave entries from the database
        /// </summary>
        /// <returns>A list of ScheduledLeave entries</returns>
        public IEnumerable<ScheduledLeave> GetAllScheduled()
        {
            return database.GetAllLeaves().OrderBy(l => l.StartDate);
        }

        /// <summary>
        /// Gets all of the ScheduledLeave entries from the database who's EndDates have already passed based on the provided date.
        /// </summary>
        /// <param name="date">The date to check against</param>
        /// <returns>A list of ScheduledLeave entries who's EndDates occur prior to the given date</returns>
        public IEnumerable<ScheduledLeave> GetPastScheduled(DateTime date)
        {
            IOrderedEnumerable<ScheduledLeave> pastLeaves = database.GetAllLeaves().Where(l => l.EndDate < date).OrderBy(l => l.StartDate);

            return pastLeaves;
        }

        /// <summary>
        /// Deletes the ScheduledLeave entry from the database if it exists.
        /// </summary>
        /// <param name="leave">The ScheduleLeave entry to delete.</param>
        public void DeleteLeave(ScheduledLeave leave)
        {
            _ = database.DeleteLeave(leave);
        }
    }
}
