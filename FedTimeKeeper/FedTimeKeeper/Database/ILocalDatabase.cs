using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Database
{
    public interface ILocalDatabase
    {
        void Initialize();

        int CreateUser(User user);
        User GetUser();
        int DeleteUser(User user);

        IEnumerable<ScheduledLeave> GetAllLeaves();
        int AddLeave(ScheduledLeave leave);
        int UpdateLeave(ScheduledLeave leave);
        int DeleteLeave(ScheduledLeave leave);
    }
}
