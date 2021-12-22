using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface ILeaveSummaryService
    {
        LeaveSummary GetAnnualLeaveSummary(DateTime startDate, DateTime endDate);
        LeaveSummary GetSickLeaveSummary(DateTime startDate, DateTime endDate);
        LeaveSummary GetTimeOffAwardSummary(DateTime startDate, DateTime endDate);
        UseOrLoseSummary GetUseOrLoseSummary(DateTime startDate, DateTime endDate);
    }
}
