using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Database
{
    public interface ILocalDatabase
    {
        void Initialize();

        IEnumerable<ScheduledLeave> GetAllLeaves();
        int CreateUser(User user);
        User GetUser();
        int DeleteUser(User user);

        int AddLeave(ScheduledLeave leave);
        int UpdateLeave(ScheduledLeave leave);
        int DeleteLeave(ScheduledLeave leave);
    }
}
