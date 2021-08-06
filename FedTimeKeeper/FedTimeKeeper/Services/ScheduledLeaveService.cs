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

        //private List<ScheduledLeave> LoadDummyData()
        //{
        //    List<ScheduledLeave> dummyLeaves = new List<ScheduledLeave>
        //    {
        //        new ScheduledLeave
        //        {
        //            StartDate = new DateTime(2021, 01, 01),
        //            EndDate = new DateTime(2021, 01, 02),
        //            HoursTaken = 8.0,
        //            Type = LeaveType.Annual
        //        },
        //        new ScheduledLeave
        //        {
        //            StartDate = new DateTime(2021, 02, 01),
        //            EndDate = new DateTime(2021, 02, 02),
        //            HoursTaken = 8.0,
        //            Type = LeaveType.Annual
        //        },
        //        new ScheduledLeave
        //        {
        //            StartDate = new DateTime(2021, 03, 01),
        //            EndDate = new DateTime(2021, 03, 02),
        //            HoursTaken = 8.0,
        //            Type = LeaveType.Timeoff
        //        },
        //        new ScheduledLeave
        //        {
        //            StartDate = new DateTime(2021, 04, 01),
        //            EndDate = new DateTime(2021, 04, 02),
        //            HoursTaken = 8.0,
        //            Type = LeaveType.Sick
        //        },
        //    };

        //    return dummyLeaves;
        //}

        //public IEnumerable<ScheduledLeaveModel> GetUpcomingScheudled()
        //{
        //    return scheduledLeaves.Where(sl => sl.EndDate >= DateTime.Now);
        //}

        //public void RemoveScheduledLeave(ScheduledLeaveModel leaveToRemove)
        //{
        //    if (scheduledLeaves.Contains(leaveToRemove))
        //    {
        //        scheduledLeaves.Remove(leaveToRemove);
        //    }
        //}
    }
}
