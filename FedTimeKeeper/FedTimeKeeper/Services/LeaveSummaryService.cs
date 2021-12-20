using FedTimeKeeper.Models;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FedTimeKeeper.Services
{

    public class LeaveSummaryService : ILeaveSummaryService
    {
        private readonly IFederalLeaveCalculator leaveCalculator;
        private readonly IFederalCalendarService calendarService;
        private readonly IScheduledLeaveService leaveService;
        private readonly ISettingsService settingsService;

        public LeaveSummaryService(ISettingsService settingsService, IScheduledLeaveService leaveService,
            IFederalLeaveCalculator leaveCalculator, IFederalCalendarService calendarService)
        {
            this.leaveService = leaveService;
            this.leaveCalculator = leaveCalculator;
            this.calendarService = calendarService;
            this.settingsService = settingsService;
        }

        public LeaveSummary GetAnnualLeaveSummary(DateTime asOfDate)
        {
            LeaveSummary annualLeaveSummary = new LeaveSummary { Type = LeaveType.Annual };
            if (!calendarService.TryGetPayPeriodForDate(asOfDate, out FederalPayPeriod currentPayPeriod))
            {
                throw new ArgumentOutOfRangeException(nameof(asOfDate), asOfDate, "Date does not exist in any current pay calendars.");
            }

            annualLeaveSummary.BeginningBalance = settingsService.AnnualLeaveStart;
            annualLeaveSummary.Earned = leaveCalculator.EndingLeaveBalance(currentPayPeriod.Period);
            annualLeaveSummary.Used = leaveService.GetHoursTaken(LeaveType.Annual, asOfDate);

            return annualLeaveSummary;
        }

        public LeaveSummary GetSickLeaveSummary(DateTime asOfDate)
        {
            LeaveSummary sickLeaveSummary = new LeaveSummary { Type = LeaveType.Sick };

            if (!calendarService.TryGetPayPeriodForDate(asOfDate, out FederalPayPeriod currentPayPeriod))
            {
                throw new ArgumentOutOfRangeException(nameof(asOfDate), asOfDate, "Date does not exist in any current pay calendars.");
            }

            sickLeaveSummary.BeginningBalance = settingsService.SickLeaveStart;
            sickLeaveSummary.Earned = leaveCalculator.EndingSickLeaveBalance(currentPayPeriod.Period);
            sickLeaveSummary.Used = leaveService.GetHoursTaken(LeaveType.Sick, asOfDate);

            return sickLeaveSummary;
        }

        public LeaveSummary GetTimeOffAwardSummary(DateTime asOfDate)
        {
            LeaveSummary timeOffSummary = new LeaveSummary
            {
                Type = LeaveType.Timeoff,
                BeginningBalance = settingsService.TimeOffStart,
                Earned = 0.0,
                Used = leaveService.GetHoursTaken(LeaveType.Timeoff, asOfDate)
            };

            return timeOffSummary;
        }

        public UseOrLoseSummary GetUseOrLoseSummary(DateTime asOfDate)
        {
            UseOrLoseSummary useOrLoseSummary = new UseOrLoseSummary
            {
                Type = LeaveType.Annual,
                BeginningBalance = 0.0,
                Earned = 0.0,
                Used = 0.0
            };

            if (!calendarService.TryGetPayCalendarForDate(asOfDate, out ICalendar currentCalendar))
            {
                throw new ArgumentOutOfRangeException(nameof(asOfDate), asOfDate, "Date does not exist in any current pay calendars.");
            }

            FederalPayPeriod finalPayPeriod = currentCalendar.GetFinalPayPeriod();

            double startBalance = settingsService.AnnualLeaveStart;

            double leaveUsed = leaveService.GetHoursTaken(LeaveType.Annual, asOfDate);

            double endOfYearBalance = leaveCalculator.EndingLeaveBalance(finalPayPeriod.Period);

            double adjustedEndOfYearBalance = startBalance + endOfYearBalance - leaveUsed;

            if (adjustedEndOfYearBalance >= Constants.MaxLeaveBalance)
            {
                useOrLoseSummary.SetBalance(adjustedEndOfYearBalance - Constants.MaxLeaveBalance);
            }

            return useOrLoseSummary;
        }
    }
}