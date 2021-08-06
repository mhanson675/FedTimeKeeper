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
        private readonly FederalPayCalendar payCalendar;
        private readonly IScheduledLeaveService leaveService;
        private readonly ISettingsService settingsService;

        public LeaveSummaryService(ISettingsService settingsService, IScheduledLeaveService leaveService,
            FedAnnualLeaveCalculator annualLeaveCalculator, FedSickLeaveCalculator sickLeaveCalculator, FederalPayCalendar payCalendar)
        {
            this.leaveService = leaveService;
            this.annualLeaveCalculator = annualLeaveCalculator;
            this.sickLeaveCalculator = sickLeaveCalculator;
            this.payCalendar = payCalendar;
            this.settingsService = settingsService;
        }

        public LeaveSummary GetAnnualLeaveSummary()
        {
            var annualLeaveSummary = new LeaveSummary { Type = LeaveType.Annual };
            var currentPayPeriod = payCalendar.GetCurrentPayPeriod(DateTime.Now);
            var scheduledLeave = leaveService.GetPastScheduled(DateTime.Now);

            annualLeaveSummary.BeginningBalance = settingsService.GetAnnualLeaveStart();
            annualLeaveSummary.Earned = annualLeaveCalculator.PayPeriodEndingLeaveBalance(currentPayPeriod);
            annualLeaveSummary.Used = scheduledLeave.Where(sl => sl.Type == LeaveType.Annual).Sum(sl => sl.HoursTaken);

            return annualLeaveSummary;
        }

        public LeaveSummary GetSickLeaveSummary()
        {
            var sickLeaveSummary = new LeaveSummary { Type = LeaveType.Sick };
            var currentPayPeriod = payCalendar.GetCurrentPayPeriod(DateTime.Now);
            var scheduledLeave = leaveService.GetPastScheduled(DateTime.Now);

            sickLeaveSummary.BeginningBalance = settingsService.GetSickLeaveStart();
            sickLeaveSummary.Earned = sickLeaveCalculator.PayPeriodEndingLeaveBalance(currentPayPeriod);
            sickLeaveSummary.Used = scheduledLeave.Where(sl => sl.Type == LeaveType.Sick).Sum(sl => sl.HoursTaken);

            return sickLeaveSummary;
        }

        public LeaveSummary GetTimeOffAwardSummary()
        {
            var timeOffSummary = new LeaveSummary { Type = LeaveType.Timeoff };
            //var currentPayPeriod = payCalendar.GetCurrentPayPeriod(DateTime.Now);
            var scheduledLeave = leaveService.GetPastScheduled(DateTime.Now);

            timeOffSummary.BeginningBalance = settingsService.GetTimeOffStart();
            timeOffSummary.Earned = 0.0;
            timeOffSummary.Used = scheduledLeave.Where(sl => sl.Type == LeaveType.Timeoff).Sum(sl => sl.HoursTaken);

            return timeOffSummary;
        }

        public UseOrLoseSummary GetUseOrLoseSummary()
        {
            var useOrLoseSummary = new UseOrLoseSummary
            {
                Type = LeaveType.Annual,
                BeginningBalance = 0.0,
                Earned = 0.0,
                Used = 0.0
            };

            var finalPayPeriod = payCalendar.GetFinalPayPeriod();
            var startBalance = settingsService.GetAnnualLeaveStart();
            var scheduledLeave = leaveService.GetPastScheduled(DateTime.Now);
            var leaveUsed = scheduledLeave.Where(sl => sl.Type == LeaveType.Annual).Sum(sl => sl.HoursTaken);

            var endOfYearBalance = annualLeaveCalculator.PayPeriodEndingLeaveBalance(finalPayPeriod);

            var adjustedEndOfYearBalance = startBalance + endOfYearBalance - leaveUsed;

            if (adjustedEndOfYearBalance >= Constants.MaxLeaveBalance)
            {
                useOrLoseSummary.SetBalance(adjustedEndOfYearBalance - Constants.MaxLeaveBalance);
            }

            return useOrLoseSummary;
        }

    }
}
