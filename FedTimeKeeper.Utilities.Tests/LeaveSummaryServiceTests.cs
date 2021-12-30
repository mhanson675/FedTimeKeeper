using FedTimeKeeper.Models;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using Moq;
using System;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class LeaveSummaryServiceTests
    {
        private readonly Mock<IFederalCalendarService> calendarServiceMock;
        private readonly Mock<IScheduledLeaveService> scheduledMock;
        private readonly Mock<ISettingsService> settingsMock;
        private readonly int accrualRate = 8;
        private readonly int sickRate = 4;
        private readonly int period = 5;
        private readonly int finalPayPeriod = 26;

        public LeaveSummaryServiceTests()
        {
            scheduledMock = new Mock<IScheduledLeaveService>();
            
            settingsMock = new Mock<ISettingsService>();
            settingsMock.Setup(x => x.AccrualRate).Returns(accrualRate);

            FederalPayPeriod payPeriod = new FederalPayPeriod(new DateTime(2022, 01, 02), period);
            FederalPayPeriod finalFederalPayPeriod = new FederalPayPeriod(new DateTime(2022, 01, 02), finalPayPeriod);
            Mock<ICalendar> calendarMock = new Mock<ICalendar>();
            calendarMock.Setup(x => x.TryGetPayPeriodForDate(It.IsAny<DateTime>(), out payPeriod)).Returns(true);
            calendarMock.Setup(x => x.GetFinalPayPeriod()).Returns(finalFederalPayPeriod);

            ICalendar outCalendar = calendarMock.Object;
            calendarServiceMock = new Mock<IFederalCalendarService>();
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
        }

        [Fact]
        public void LeaveSummaryService_Annual_Success()
        {
            int expectedEarned = period * accrualRate;
            int expectedTaken = 32;
            int expectedBalance = expectedEarned - expectedTaken;
            settingsMock.Setup(x => x.AnnualLeaveStart).Returns(0);
            scheduledMock.Setup(x => x.GetHoursTaken(It.IsAny<DateTime>(), It.IsAny<DateTime>(), LeaveType.Annual)).Returns(expectedTaken);
            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, scheduledMock.Object, calendarServiceMock.Object);

            LeaveSummary summary = sut.GetSummary(DateTime.Now, LeaveType.Annual);

            Assert.NotNull(summary);
            Assert.Equal(0, summary.BeginningBalance);
            Assert.Equal(expectedEarned, summary.Earned);
            Assert.Equal(LeaveType.Annual, summary.Type);
            Assert.Equal(expectedTaken, summary.Used);
            Assert.Equal(expectedBalance, summary.EndingBalance);
        }

        [Fact]
        public void LeaveSummaryService_Sick_Success()
        {
            int expectedEarned = sickRate * period;
            int expectedTaken = 4;
            int expectedBalance = expectedEarned - expectedTaken;
            settingsMock.Setup(x => x.SickLeaveStart).Returns(0);
            scheduledMock.Setup(x => x.GetHoursTaken(It.IsAny<DateTime>(), It.IsAny<DateTime>(), LeaveType.Sick)).Returns(expectedTaken);

            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, scheduledMock.Object, calendarServiceMock.Object);

            LeaveSummary summary = sut.GetSummary(DateTime.Now, LeaveType.Sick);

            Assert.NotNull(summary);
            Assert.Equal(0, summary.BeginningBalance);
            Assert.Equal(expectedEarned, summary.Earned);
            Assert.Equal(LeaveType.Sick, summary.Type);
            Assert.Equal(expectedTaken, summary.Used);
            Assert.Equal(expectedBalance, summary.EndingBalance);
        }

        [Fact]
        public void LeaveSummaryService_TimeOff_Success()
        {
            int expectedBegin = 24;
            int expectedUsed = 16;
            int expectedEnd = expectedBegin - expectedUsed;
            settingsMock.Setup(x => x.TimeOffStart).Returns(expectedBegin);
            scheduledMock.Setup(x => x.GetHoursTaken(It.IsAny<DateTime>(), It.IsAny<DateTime>(), LeaveType.Timeoff)).Returns(expectedUsed);
            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, scheduledMock.Object, calendarServiceMock.Object);

            LeaveSummary summary = sut.GetSummary(DateTime.Now, LeaveType.Timeoff);

            Assert.NotNull(summary);
            Assert.Equal(expectedBegin, summary.BeginningBalance);
            Assert.Equal(0, summary.Earned);
            Assert.Equal(LeaveType.Timeoff, summary.Type);
            Assert.Equal(expectedUsed, summary.Used);
            Assert.Equal(expectedEnd, summary.EndingBalance);
        }

        [Fact]
        public void LeaveSummaryService_UseOrLose_Success()
        {
            int annualStart = Constants.MaxLeaveBalance;
            int annualEarned = accrualRate * finalPayPeriod;
            int annualTaken = 32;
            int expectedEnd = annualEarned - annualTaken;

            settingsMock.Setup(x => x.AnnualLeaveStart).Returns(annualStart);
            scheduledMock.Setup(x => x.GetHoursTaken(It.IsAny<DateTime>(), It.IsAny<DateTime>(), LeaveType.Annual)).Returns(annualTaken);
            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, scheduledMock.Object, calendarServiceMock.Object);

            LeaveSummary summary = sut.GetUseOrLoseSummary(DateTime.Now);

            Assert.NotNull(summary);
            Assert.Equal(0, summary.BeginningBalance);
            Assert.Equal(0, summary.Earned);
            Assert.Equal(LeaveType.Annual, summary.Type);
            Assert.Equal(0, summary.Used);
            Assert.Equal(expectedEnd, summary.EndingBalance);
        }
    }
}