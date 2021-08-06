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

            database.CreateTable<ScheduledLeave>();
        }

        public IEnumerable<ScheduledLeave> GetAllLeaves()
        {
            return database.Table<ScheduledLeave>().ToList();
        }

        //public List<ScheduledLeave> GetUpcomingLeaves(DateTime date)
        //{
        //    return database.Table<ScheduledLeave>().Where(l => l.StartDate > date).OrderBy(l => l.StartDate).ToList();
        //}

        //public List<ScheduledLeave> GetTakenLeaves(DateTime date)
        //{
        //    return database.Table<ScheduledLeave>().Where(l => l.EndDate <= date).OrderBy(l => l.StartDate).ToList();
        //}

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
    }
}
