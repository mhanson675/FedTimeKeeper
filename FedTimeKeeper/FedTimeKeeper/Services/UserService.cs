using FedTimeKeeper.Database;
using FedTimeKeeper.Models;
using FedTimeKeeper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services
{
    public class UserService : IUserService
    {
        private readonly ILocalDatabase database;

        public UserService(ILocalDatabase database)
        {
            this.database = database;
        }

        public void AddUser(User user)
        {
            _ = database.CreateUser(user);
        }

        public void DeleteUser(User user)
        {
            _ = database.DeleteUser(user);
        }

        public User GetUser()
        {
            return database.GetUser();
        }
    }
}
