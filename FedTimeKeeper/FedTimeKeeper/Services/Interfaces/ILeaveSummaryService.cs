using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface ILeaveSummaryService
    {
        public LeaveSummary GetSummary(DateTime asOfDate, LeaveType leaveType);
        public UseOrLoseSummary GetUseOrLoseSummary(DateTime asOfDate);
    }
}
