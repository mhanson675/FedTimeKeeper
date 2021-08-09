using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface ILeaveSummaryService
    {
        LeaveSummary GetAnnualLeaveSummary(DateTime asOfDate);
        LeaveSummary GetSickLeaveSummary(DateTime asOfDate);
        LeaveSummary GetTimeOffAwardSummary(DateTime asOfDate);
        UseOrLoseSummary GetUseOrLoseSummary(DateTime asOfDate);
    }
}
