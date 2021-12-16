using FedTimeKeeper.Models;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FedTimeKeeper.Services
{
    public class LeaveSummaryService : ILeaveSummaryService
    {
        private readonly FedAnnualLeaveCalculator annualLeaveCalculator;
        private readonly FedSickLeaveCalculator sickLeaveCalculator;
        private readonly FederalCalendarService calendarService;
        private readonly IScheduledLeaveService leaveService;
        private readonly ISettingsService settingsService;

        public LeaveSummaryService(ISettingsService settingsService, IScheduledLeaveService leaveService,
            FedAnnualLeaveCalculator annualLeaveCalculator, FedSickLeaveCalculator sickLeaveCalculator, FederalCalendarService calendarService)
        {
            this.leaveService = leaveService;
            this.annualLeaveCalculator = annualLeaveCalculator;
            this.sickLeaveCalculator = sickLeaveCalculator;
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

            IEnumerable<ScheduledLeave> scheduledLeave = leaveService.GetPastScheduled(asOfDate);

            annualLeaveSummary.BeginningBalance = settingsService.AnnualLeaveStart;
            annualLeaveSummary.Earned = annualLeaveCalculator.PayPeriodEndingLeaveBalance(currentPayPeriod);
            annualLeaveSummary.Used = scheduledLeave.Where(sl => sl.Type == LeaveType.Annual).Sum(sl => sl.HoursTaken);

            return annualLeaveSummary;
        }

        public LeaveSummary GetSickLeaveSummary(DateTime asOfDate)
        {
            LeaveSummary sickLeaveSummary = new LeaveSummary { Type = LeaveType.Sick };
            if (!calendarService.TryGetPayPeriodForDate(asOfDate, out FederalPayPeriod currentPayPeriod))
            {
                throw new ArgumentOutOfRangeException(nameof(asOfDate), asOfDate, "Date does not exist in any current pay calendars.");
            }

            IEnumerable<ScheduledLeave> scheduledLeave = leaveService.GetPastScheduled(asOfDate);

            sickLeaveSummary.BeginningBalance = settingsService.SickLeaveStart;
            sickLeaveSummary.Earned = sickLeaveCalculator.PayPeriodEndingLeaveBalance(currentPayPeriod);
            sickLeaveSummary.Used = scheduledLeave.Where(sl => sl.Type == LeaveType.Sick).Sum(sl => sl.HoursTaken);

            return sickLeaveSummary;
        }

        public LeaveSummary GetTimeOffAwardSummary(DateTime asOfDate)
        {
            LeaveSummary timeOffSummary = new LeaveSummary { Type = LeaveType.Timeoff };
            IEnumerable<ScheduledLeave> scheduledLeave = leaveService.GetPastScheduled(asOfDate);

            timeOffSummary.BeginningBalance = settingsService.TimeOffStart;
            timeOffSummary.Earned = 0.0;
            timeOffSummary.Used = scheduledLeave.Where(sl => sl.Type == LeaveType.Timeoff).Sum(sl => sl.HoursTaken);

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

            if (!calendarService.TryGetPayCalendarForDate(asOfDate, out FederalPayCalendar currentCalendar))
            {
                throw new ArgumentOutOfRangeException(nameof(asOfDate), asOfDate, "Date does not exist in any current pay calendars.");
            }

            FederalPayPeriod finalPayPeriod = currentCalendar.GetFinalPayPeriod();

            double startBalance = settingsService.AnnualLeaveStart;
            IEnumerable<ScheduledLeave> scheduledLeave = leaveService.GetPastScheduled(asOfDate);
            double leaveUsed = scheduledLeave.Where(sl => sl.Type == LeaveType.Annual).Sum(sl => sl.HoursTaken);

            double endOfYearBalance = annualLeaveCalculator.PayPeriodEndingLeaveBalance(finalPayPeriod);

            double adjustedEndOfYearBalance = startBalance + endOfYearBalance - leaveUsed;

            if (adjustedEndOfYearBalance >= Constants.MaxLeaveBalance)
            {
                useOrLoseSummary.SetBalance(adjustedEndOfYearBalance - Constants.MaxLeaveBalance);
            }

            return useOrLoseSummary;
        }
    }
}