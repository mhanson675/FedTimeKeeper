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
        public DateTime SettingsDate
        {
            get => Preferences.Get(Constants.SettingsDateKey, Constants.DefaultDate);
            set => Preferences.Set(Constants.SettingsDateKey, value);
        }
        public int AccrualRate
        {
            get => Preferences.Get(Constants.LeaveRateKey, Constants.DefaultLeaveAccrual);
            set => Preferences.Set(Constants.LeaveRateKey, value);
        }

        public double AnnualLeaveStart
        {
            get => Preferences.Get(Constants.AnnualLeaveKey, 0.0);
            set => Preferences.Set(Constants.AnnualLeaveKey, value);
        }

        public double SickLeaveStart
        {
            get => Preferences.Get(Constants.SickLeaveKey, 0.0);
            set => Preferences.Set(Constants.SickLeaveKey, value);
        }

        public double TimeOffStart
        {
            get => Preferences.Get(Constants.TimeOffKey, 0.0);
            set => Preferences.Set(Constants.TimeOffKey, value);
        }
    }
}
