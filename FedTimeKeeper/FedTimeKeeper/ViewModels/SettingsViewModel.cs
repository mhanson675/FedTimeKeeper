using FedTimeKeeper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FedTimeKeeper.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ISettingsService settings;

        public ICommand SaveCommand => new Command(OnSaveCommand);

        private double startingAnnualBalance;

        public double StartingAnnualBalance
        {
            get => startingAnnualBalance;
            set
            {
                startingAnnualBalance = value;
                OnPropertyChanged(nameof(StartingAnnualBalance));
            }
        }

        private double startingSickBalance;

        public double StartingSickBalance
        {
            get => startingSickBalance;
            set
            {
                startingSickBalance = value;
                OnPropertyChanged(nameof(StartingSickBalance));
            }
        }

        private double startingTimeOffBalance;

        public double StartingTimeOffBalance
        {
            get => startingTimeOffBalance;
            set
            {
                startingTimeOffBalance = value;
                OnPropertyChanged(nameof(StartingTimeOffBalance));
            }
        }

        private DateTime firstPayPeriodStartDate;

        public DateTime FirstPayPeriodStartDate
        {
            get => firstPayPeriodStartDate;
            set
            {
                firstPayPeriodStartDate = value;
                OnPropertyChanged(nameof(FirstPayPeriodStartDate));
            }
        }

        private DateTime lastPayPeriodStartDate;

        public DateTime LastPayPeriodStartDate
        {
            get => lastPayPeriodStartDate;
            set
            {
                lastPayPeriodStartDate = value;
                OnPropertyChanged(nameof(LastPayPeriodStartDate));
            }
        }

        private int leaveAccrualRate;

        public int LeaveAccrualRate
        {
            get => leaveAccrualRate;
            set
            {
                leaveAccrualRate = value;
                OnPropertyChanged(nameof(LeaveAccrualRate));
            }
        }

        public SettingsViewModel(ISettingsService settings)
        {
            this.settings = settings;
            LoadSettings();
        }

        private void LoadSettings()
        {
            StartingAnnualBalance = settings.GetAnnualLeaveStart();
            StartingSickBalance = settings.GetSickLeaveStart();
            StartingTimeOffBalance = settings.GetTimeOffStart();
            FirstPayPeriodStartDate = settings.GetFirstPayPeriodStart();
            LastPayPeriodStartDate = settings.GetLastPayPeriodStart();
            LeaveAccrualRate = settings.GetAccrualRate();
        }

        private void OnSaveCommand()
        {
        }
    }
}