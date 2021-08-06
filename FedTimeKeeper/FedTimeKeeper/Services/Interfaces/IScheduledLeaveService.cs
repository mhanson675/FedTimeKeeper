using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface IScheduledLeaveService
    {
        IEnumerable<ScheduledLeave> GetAllScheduled();

        IEnumerable<ScheduledLeave> GetPastScheduled(DateTime date);

        void DeleteLeave(ScheduledLeave leave);
        void SaveLeave(ScheduledLeave leave);
    }
}
