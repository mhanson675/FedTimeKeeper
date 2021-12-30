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
        private readonly IFederalCalendarService calendarService;
        private readonly IScheduledLeaveService leaveService;
        private readonly ISettingsService settingsService;

        public LeaveSummaryService(ISettingsService settingsService, IScheduledLeaveService leaveService, IFederalCalendarService calendarService)
        {
            this.leaveService = leaveService;
            this.calendarService = calendarService;
            this.settingsService = settingsService;
        }

        public LeaveSummary GetSummary(DateTime asOfDate, LeaveType leaveType)
        {
            if (!calendarService.TryGetPayCalendarForDate(asOfDate, out ICalendar calendar))
            {
                throw new ArgumentOutOfRangeException(nameof(asOfDate), asOfDate, "Date does not exist in any current pay calendars.");
            }

            if (!calendar.TryGetPayPeriodForDate(asOfDate, out FederalPayPeriod currentPayPeriod))
            {
                throw new ArgumentOutOfRangeException(nameof(asOfDate), asOfDate, "Date does not exist in any current pay calendars.");
            }


            double beginningBalance = PayYearBeginningBalance(leaveType);
            double earned = EndOfPayPeriodAccrued(currentPayPeriod.Period, leaveType);
            double used = leaveService.GetHoursTaken(calendar.StartDate, asOfDate, leaveType);

            return new LeaveSummary {Type = leaveType, BeginningBalance = beginningBalance, Earned = earned, Used = used};
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
            double leaveUsed = leaveService.GetHoursTaken(currentCalendar.StartDate, asOfDate, LeaveType.Annual);
            double endOfYearBalance = EndOfPayPeriodAccrued(finalPayPeriod.Period, LeaveType.Annual);
            double adjustedEndOfYearBalance = startBalance + endOfYearBalance - leaveUsed;

            if (adjustedEndOfYearBalance >= Constants.MaxLeaveBalance)
            {
                useOrLoseSummary.SetBalance(adjustedEndOfYearBalance - Constants.MaxLeaveBalance);
            }

            return useOrLoseSummary;
        }

        private double PayYearBeginningBalance(LeaveType leaveType)
        {
            return leaveType switch
            {
                LeaveType.Annual => settingsService.AnnualLeaveStart,
                LeaveType.Sick => settingsService.SickLeaveStart,
                LeaveType.Timeoff => settingsService.TimeOffStart,
                _ => throw new ArgumentOutOfRangeException(nameof(leaveType), leaveType, null)
            };
        }
        private double EndOfPayPeriodAccrued(int period, LeaveType leaveType)
        {
            return leaveType switch
            {
                LeaveType.Annual => period * settingsService.AccrualRate,
                LeaveType.Sick => period * 4,
                LeaveType.Timeoff => 0,
                _ => throw new ArgumentOutOfRangeException(nameof(leaveType), leaveType, null)
            };
        }
    }
}