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
    public class ScheduledLeaveService : IScheduledLeaveService
    {
        private readonly ILocalDatabase database;

        public ScheduledLeaveService(ILocalDatabase database)
        {
            this.database = database;
        }


        public void SaveLeave(ScheduledLeave leave)
        {
            if (leave?.Id == 0)
            {
                _ = database.AddLeave(leave);
            }
            else
            {
                _ = database.UpdateLeave(leave);
            }
        }

        public IEnumerable<ScheduledLeave> GetAllScheduled()
        {
            return database.GetAllLeaves().OrderBy(l => l.StartDate);
        }

        public IEnumerable<ScheduledLeave> GetPastScheduled(DateTime date)
        {
            IOrderedEnumerable<ScheduledLeave> pastLeaves = database.GetAllLeaves().Where(l => l.EndDate < date).OrderBy(l => l.StartDate);

            return pastLeaves;
        }

        public void DeleteLeave(ScheduledLeave leave)
        {
            _ = database.DeleteLeave(leave);
        }
    }
}
