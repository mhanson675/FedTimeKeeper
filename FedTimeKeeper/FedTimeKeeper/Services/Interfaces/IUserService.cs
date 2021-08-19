using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface IUserService
    {
        void AddUser(User user);
        User GetUser();
        void DeleteUser(User user);
    }
}
