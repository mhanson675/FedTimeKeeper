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
        public int AccrualRate
        {
            get => Preferences.Get(Constants.leaveRateKey, Constants.DefaultLeaveAccrual);
            set => Preferences.Set(Constants.leaveRateKey, value);
        }

        public double AnnualLeaveStart
        {
            get => Preferences.Get(Constants.annualLeaveKey, 0.0);
            set => Preferences.Set(Constants.annualLeaveKey, value);
        }

        public double SickLeaveStart
        {
            get => Preferences.Get(Constants.sickLeaveKey, 0.0);
            set => Preferences.Set(Constants.sickLeaveKey, value);
        }

        public double TimeOffStart
        {
            get => Preferences.Get(Constants.timeOffKey, 0.0);
            set => Preferences.Set(Constants.timeOffKey, value);
        }
    }
}
