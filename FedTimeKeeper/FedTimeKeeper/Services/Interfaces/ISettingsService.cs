using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface ISettingsService
    {
        DateTime SettingsDate { get; set; }
        int AccrualRate { get; set; }
        double AnnualLeaveStart { get; set; }
        double SickLeaveStart { get; set; }
        double TimeOffStart { get; set; }
    }
}
