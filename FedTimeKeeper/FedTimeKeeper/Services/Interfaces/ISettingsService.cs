using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface ISettingsService
    {
        int AccrualRate { get; set; }
        double AnnualLeaveStart { get; set; }
        double SickLeaveStart { get; set; }
        double TimeOffStart { get; set; }
    }
}
