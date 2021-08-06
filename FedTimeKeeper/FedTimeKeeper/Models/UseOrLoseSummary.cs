using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Models
{
    public class UseOrLoseSummary : LeaveSummary
    {
        private double balance;
        public override double EndingBalance => balance;

        public void SetBalance(double balance)
        {
            this.balance = balance;
        }
    }
}
