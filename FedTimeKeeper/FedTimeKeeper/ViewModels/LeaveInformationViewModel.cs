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
        private readonly IFederalCalendarService calendarService;
        private readonly ILeaveSummaryService leaveSummaryService;

        private DateTime firstDayOfPayYear;

        public DateTime FirstDayOfPayYear
        {
            get => firstDayOfPayYear;
            set
            {
                firstDayOfPayYear = value;
                OnPropertyChanged(nameof(FirstDayOfPayYear));
            }
        }

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

        public LeaveInformationViewModel(IFederalCalendarService calendarService, ILeaveSummaryService leaveSummaryService, INavigationService navigation)
        {
            this.calendarService = calendarService;
            this.leaveSummaryService = leaveSummaryService;
            this.navigation = navigation;
            Annual = new LeaveSummary();
            UseOrLose = new LeaveSummary();
            Sick = new LeaveSummary();
            TimeOff = new LeaveSummary();
            FirstDayOfPayYear = DateTime.Now;

            LoadData();
        }

        private void LoadData()
        {
            if (!calendarService.TryGetPayCalendarForDate(DateTime.Now, out FederalPayCalendar currentCalendar))
            {
                throw new ArgumentOutOfRangeException(nameof(calendarService), calendarService, "There is not Pay Calendar for today's date.");
            }

            FirstDayOfPayYear = currentCalendar.StartDate;

            if (currentCalendar.TryGetPayPeriodForDate(DateTime.Now, out FederalPayPeriod currentPayPeriod))
            {
                throw new ArgumentOutOfRangeException(nameof(calendarService), calendarService, "There is not Pay Calendar for today's date.");
            }

            AsOfDate = currentPayPeriod.EndDate;

            Annual = leaveSummaryService.GetAnnualLeaveSummary(ReportPayPeriodEndDate);
            UseOrLose = leaveSummaryService.GetUseOrLoseSummary(ReportPayPeriodEndDate);
            Sick = leaveSummaryService.GetSickLeaveSummary(ReportPayPeriodEndDate);
            TimeOff = leaveSummaryService.GetTimeOffAwardSummary(ReportPayPeriodEndDate);
        }

        private void UpdateReportEndingDate(DateTime newDate)
        {
            if (!calendarService.TryGetPreviousPayPeriod(newDate, out FederalPayPeriod previoudPayPeriod))
            {
                return;
            }

            ReportPayPeriodEndDate = previoudPayPeriod.EndDate;
        }
    }
}