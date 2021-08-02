using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Utilities
{
    public static class Constants
    {
        public static readonly DateTime BasePayPeriod = new DateTime(2009, 12, 20);
        public const string startingLeaveKey = "StartLeaveBalance";
        public const string leaveRateKey = "LeaveAccrualRate";
        public const string firstDateKey = "FirstStartDate";
        public const string lastDateKey = "LastStartDate";
    }
}