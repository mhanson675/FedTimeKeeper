using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface ILeaveSummaryService
    {
        LeaveSummary GetAnnualLeaveSummary();
        LeaveSummary GetSickLeaveSummary();
        LeaveSummary GetTimeOffAwardSummary();
        UseOrLoseSummary GetUseOrLoseSummary();
    }
}
