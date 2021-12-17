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
        
        //TODO: Remove??
        public ICommand SaveCommand => new Command(OnSaveCommand);


        public List<int> LeaveAccrualRates { get; set; }

        private double startingAnnualBalance;
        public double StartingAnnualBalance
        {
            get => startingAnnualBalance;
            set
            {
                startingAnnualBalance = value;
                OnPropertyChanged(nameof(StartingAnnualBalance));
                settings.AnnualLeaveStart = StartingAnnualBalance;
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
                settings.SickLeaveStart = StartingSickBalance;
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
                settings.TimeOffStart = StartingTimeOffBalance;
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
                settings.AccrualRate = LeaveAccrualRate;
            }
        }

        public SettingsViewModel(ISettingsService settings)
        {
            this.settings = settings;
            LeaveAccrualRates = new List<int> { 4, 6, 8 };
            LoadSettings();
        }

        private void LoadSettings()
        {
            StartingAnnualBalance = settings.AnnualLeaveStart;
            StartingSickBalance = settings.SickLeaveStart;
            StartingTimeOffBalance = settings.TimeOffStart;
            LeaveAccrualRate = settings.AccrualRate;
        }

        //TODO: REMOVE??
        private void OnSaveCommand()
        {
        }
    }
}