using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Services.Interfaces
{
    public interface ISettingsService
    {
        DateTime GetFirstPayPeriodStart();

        DateTime GetLastPayPeriodStart();

        int GetAccrualRate();

        double GetAnnualLeaveStart();

        double GetSickLeaveStart();

        double GetTimeOffStart();
    }
}
