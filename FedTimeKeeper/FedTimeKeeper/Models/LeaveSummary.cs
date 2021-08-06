using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Models
{
    public class LeaveSummary
    {
        public LeaveType Type { get; set; }
        public virtual double BeginningBalance { get; set; }
        public virtual double Earned { get; set; }
        public virtual double Used { get; set; }
        public virtual double EndingBalance => CurrentBalance();

        private double CurrentBalance()
        {
            return BeginningBalance + Earned - Used;
        }
    }
}
