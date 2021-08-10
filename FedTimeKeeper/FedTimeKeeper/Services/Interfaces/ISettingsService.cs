using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface ISettingsService
    {
        DateTime FirstPayPeriodStart { get; set; }
        DateTime LastPayPeriodStart { get; set; }
        int AccrualRate { get; set; }
        double AnnualLeaveStart { get; set; }
        double SickLeaveStart { get; set; }
        double TimeOffStart { get; set; }
        
        //DateTime GetFirstPayPeriodStart();

        //DateTime GetLastPayPeriodStart();

        //int GetAccrualRate();

        //double GetAnnualLeaveStart();

        //double GetSickLeaveStart();

        //double GetTimeOffStart();
    }
}
