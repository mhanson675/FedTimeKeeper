using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Models
{
    public enum LeaveType { Annual, Sick, Timeoff }

    public class ScheduledLeave
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double HoursTaken { get; set; }
        public LeaveType Type { get; set; }

        //public ScheduledLeave()
        //{
        //}

        //public ScheduledLeave(DateTime startDate, DateTime endDate, double hours, LeaveType type)
        //{
        //    if (startDate > endDate)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(startDate));
        //    }
        //    StartDate = startDate;
        //    EndDate = endDate;

        //    if (hours < 0)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(hours));
        //    }

        //    HoursTaken = hours;
        //    Type = type;
        //}
    }
}