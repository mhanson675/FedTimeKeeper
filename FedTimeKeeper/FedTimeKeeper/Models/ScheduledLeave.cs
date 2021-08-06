using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Models
{
    public enum LeaveType { Annual, Sick, Timeoff }

    public class ScheduledLeave
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double HoursTaken { get; set; }
        public LeaveType Type { get; set; }
    }
}