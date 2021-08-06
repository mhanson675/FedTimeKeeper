using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FedTimeKeeper.Utilities
{
    public static class Constants
    {
        public const string DatabaseFileName = "FedLeave.db3";
        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;
        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFileName);
            }
        }

        public static readonly DateTime DefaultFirstPayPeriodStart = new DateTime(2009, 12, 20);
        public static readonly DateTime DefaultLastPayPeriodStart = new DateTime(2010, 12, 19);
        public static readonly int DefaultLeaveAccrual = 4;

        public const int MaxLeaveBalance = 240;
        public const string annualLeaveKey = "StartAnnualBalance";
        public const string sickLeaveKey = "StartSickBalance";
        public const string timeOffKey = "StartTimeOffBalance";
        public const string leaveRateKey = "LeaveAccrualRate";
        public const string firstDateKey = "FirstStartDate";
        public const string lastDateKey = "LastStartDate";
    }
}