using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace FedTimeKeeper.Models
{
    public class PayYear
    {
        [PrimaryKey]
        public int CalendarYear { get; private set; }
        public double AnnualLeaveStartingBalance { get; private set; }
        public double SickLeaveStartingBalance { get; private set; }
    }
}
