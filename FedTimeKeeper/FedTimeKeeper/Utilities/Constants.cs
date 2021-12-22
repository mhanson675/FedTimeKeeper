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
                string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFileName);
            }
        }

        public static readonly int DefaultLeaveAccrual = 4;
        public static DateTime DefaultDate = new DateTime(2021, 01, 03);

        public const int MaxLeaveBalance = 240;
        public static string SettingsDateKey = "SettingsDate";
        public const string AnnualLeaveKey = "StartAnnualBalance";
        public const string SickLeaveKey = "StartSickBalance";
        public const string TimeOffKey = "StartTimeOffBalance";
        public const string LeaveRateKey = "LeaveAccrualRate";
    }
}