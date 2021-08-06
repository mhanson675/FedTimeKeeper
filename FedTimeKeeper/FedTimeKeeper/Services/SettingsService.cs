using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace FedTimeKeeper.Services
{
    public class SettingsService : ISettingsService
    {
        public DateTime GetFirstPayPeriodStart()
        {
            var startDate = Preferences.Get(Constants.firstDateKey, Constants.DefaultFirstPayPeriodStart);
            return startDate;
        }

        public DateTime GetLastPayPeriodStart()
        {
            var startDate = Preferences.Get(Constants.lastDateKey, Constants.DefaultFirstPayPeriodStart);
            return startDate;
        }

        public int GetAccrualRate()
        {
            var accrualRate = Preferences.Get(Constants.leaveRateKey, Constants.DefaultLeaveAccrual);
            return accrualRate;
        }

        public double GetAnnualLeaveStart()
        {
            return ParseLeave(Constants.annualLeaveKey);
        }

        public double GetSickLeaveStart()
        {
            return ParseLeave(Constants.sickLeaveKey);
        }

        public double GetTimeOffStart()
        {
            return ParseLeave(Constants.timeOffKey);
        }

        private double ParseLeave(string leaveKey)
        {
            var savedLeave = Preferences.Get(leaveKey, "0.00");

            if (double.TryParse(savedLeave, out double parsedLeave))
            {
                return parsedLeave;
            }
            else
            {
                return 0.0;
            }
        }
    }
}
