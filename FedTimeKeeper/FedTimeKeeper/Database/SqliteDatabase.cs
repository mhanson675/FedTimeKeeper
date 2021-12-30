using FedTimeKeeper.Models;
using FedTimeKeeper.Utilities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

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
            _ = database.CreateTable<PayYear>();
        }

        #region Leave CRUD
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
        #endregion

        #region PayYearCrud
        public IEnumerable<PayYear> GetAllPayYears()
        {
            return database.Table<PayYear>().ToList();
        }

        public int AddPayYear(PayYear payYear)
        {
            return database.Insert(payYear);
        }

        public int UpdatePayYear(PayYear payYear)
        {
            return database.Update(payYear);
        }

        public int DeletePayYear(PayYear payYear)
        {
            return database.Delete(payYear);
        }
        #endregion

        #region User CRUD
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
        #endregion
    }
}
