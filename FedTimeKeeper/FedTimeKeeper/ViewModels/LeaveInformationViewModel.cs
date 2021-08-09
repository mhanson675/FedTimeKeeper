using FedTimeKeeper.Models;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.ViewModels
{
    public class LeaveInformationViewModel : BaseViewModel
    {
        private readonly INavigationService navigation;
        private readonly ILeaveSummaryService leaveSummaryService;
        
        private LeaveSummary annual;
        public LeaveSummary Annual
        {
            get => annual;
            set
            {
                annual = value;
                OnPropertyChanged(nameof(Annual));
            }
        }

        private LeaveSummary useOrLose;
        public LeaveSummary UseOrLose
        {
            get => useOrLose;
            set
            {
                useOrLose = value;
                OnPropertyChanged(nameof(UseOrLose));
            }
        }

        private LeaveSummary sick;
        public LeaveSummary Sick
        {
            get => sick;
            set
            {
                sick = value;
                OnPropertyChanged(nameof(Sick));
            }
        }

        private LeaveSummary timeOff;
        public LeaveSummary TimeOff
        {
            get => timeOff;
            set
            {
                timeOff = value;
                OnPropertyChanged(nameof(TimeOff));
            }
        }

        public LeaveInformationViewModel(ILeaveSummaryService leaveSummaryService, INavigationService navigation)
        {
            this.leaveSummaryService = leaveSummaryService;
            this.navigation = navigation;
            Annual = new LeaveSummary();
            UseOrLose = new LeaveSummary();
            Sick = new LeaveSummary();
            TimeOff = new LeaveSummary();
            LoadData();
        }

        private void LoadData()
        {
            Annual = leaveSummaryService.GetAnnualLeaveSummary();
            UseOrLose = leaveSummaryService.GetUseOrLoseSummary();
            Sick = leaveSummaryService.GetSickLeaveSummary();
            TimeOff = leaveSummaryService.GetTimeOffAwardSummary();
        }
    }
}
