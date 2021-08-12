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
        private readonly FederalPayCalendar payCalendar;
        private readonly ILeaveSummaryService leaveSummaryService;


        private DateTime asOfDate;
        public DateTime AsOfDate
        {
            get => asOfDate;
            set
            {
                asOfDate = value;
                OnPropertyChanged(nameof(AsOfDate));
                UpdateReportEndingDate(value);
                LoadData();
            }
        }

        private DateTime reportPayPeriodEndDate;
        public DateTime ReportPayPeriodEndDate
        {
            get => reportPayPeriodEndDate;
            set
            {
                reportPayPeriodEndDate = value;
                OnPropertyChanged(nameof(ReportPayPeriodEndDate));
            }
        }

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

        public LeaveInformationViewModel(FederalPayCalendar payCalendar, ILeaveSummaryService leaveSummaryService, INavigationService navigation)
        {
            this.payCalendar = payCalendar;
            this.leaveSummaryService = leaveSummaryService;
            this.navigation = navigation;

            AsOfDate = payCalendar.GetPayPeriodEndDate(DateTime.Today);
            Annual = new LeaveSummary();
            UseOrLose = new LeaveSummary();
            Sick = new LeaveSummary();
            TimeOff = new LeaveSummary();

            LoadData();
        }

        private void LoadData()
        {
            Annual = leaveSummaryService.GetAnnualLeaveSummary(AsOfDate);
            UseOrLose = leaveSummaryService.GetUseOrLoseSummary(AsOfDate);
            Sick = leaveSummaryService.GetSickLeaveSummary(AsOfDate);
            TimeOff = leaveSummaryService.GetTimeOffAwardSummary(AsOfDate);
        }

        private void UpdateReportEndingDate(DateTime newDate)
        {
            ReportPayPeriodEndDate = payCalendar.GetPreviousPayPeriod(newDate).EndDate;
        }
    }
}
