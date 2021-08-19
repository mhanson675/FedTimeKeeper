using FedTimeKeeper.Models;
using FedTimeKeeper.Utilities;
using SQLite;
using System;
using System.Collections.Generic;

namespace FedTimeKeeper.Database
{
    public class SqliteDatabase : ILocalDatabase
    {
        private SQLiteConnection database;
        public void Initialize()
        {
            if (database == null)
            {
                string dbPath = Constants.DatabasePath;
                database = new SQLiteConnection(dbPath);
            }

            _ = database.CreateTable<ScheduledLeave>();
            _ = database.CreateTable<User>();
        }


        public IEnumerable<ScheduledLeave> GetAllLeaves()
        {
            return database.Table<ScheduledLeave>().ToList();
        }

        public int AddLeave(ScheduledLeave leave)
        {
            return database.Insert(leave);
        }

        public int UpdateLeave(ScheduledLeave leaveToUpdate)
        {
            if (leaveToUpdate.Id != 0)
            {
                return database.Update(leaveToUpdate);
            }

            return -1;
        }

        public int DeleteLeave(ScheduledLeave leave)
        {
            return database.Delete(leave);
        }


        public int CreateUser(User user)
        {
            return database.Insert(user);
        }

        public User GetUser()
        {
            return database.Table<User>().FirstOrDefault();
        }

        public int DeleteUser(User user)
        {
            return database.Delete(user);
        }
    }
}
