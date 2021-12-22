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
        private readonly Mock<IFederalLeaveCalculator> calculatorMock;
        private readonly Mock<IFederalCalendarService> calendarMock;
        private readonly Mock<IScheduledLeaveService> scheduledMock;
        private readonly Mock<ISettingsService> settingsMock;
        public LeaveSummaryServiceTests()
        {
            settingsMock = new Mock<ISettingsService>();

            calculatorMock = new Mock<IFederalLeaveCalculator>();

            scheduledMock = new Mock<IScheduledLeaveService>();

            FederalPayPeriod payPeriod = new FederalPayPeriod(new DateTime(2022, 01, 02), 1);
            calendarMock = new Mock<IFederalCalendarService>();
            calendarMock.Setup(x => x.TryGetPayPeriodForDate(It.IsAny<DateTime>(), out payPeriod)).Returns(true);
        }

        [Fact]
        public void LeaveSummaryService_Annual_Success()
        {
            const int annualAccrued = 40;
            const int annualTaken = 32;
            settingsMock.Setup(x => x.AnnualLeaveStart).Returns(0);
            calculatorMock.Setup(x => x.EndingLeaveBalance(It.IsAny<int>())).Returns(annualAccrued);
            scheduledMock.Setup(x => x.GetHoursTaken(It.Is<LeaveType>(t => t == LeaveType.Annual), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(annualTaken);
            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, scheduledMock.Object, calculatorMock.Object, calendarMock.Object);

            LeaveSummary summary = sut.GetAnnualLeaveSummary(DateTime.Now);

            Assert.NotNull(summary);
            Assert.Equal(0, summary.BeginningBalance);
            Assert.Equal(annualAccrued, summary.Earned);
            Assert.Equal(LeaveType.Annual, summary.Type);
            Assert.Equal(annualTaken, summary.Used);
            Assert.Equal(8, summary.EndingBalance);
        }

        [Fact]
        public void LeaveSummaryService_Sick_Success()
        {
            const int sickAccrued = 100;
            const int sickTaken = 10;
            const int sickBalance = sickAccrued - sickTaken;
            settingsMock.Setup(x => x.SickLeaveStart).Returns(0);
            calculatorMock.Setup(x => x.EndingSickLeaveBalance(It.IsAny<int>())).Returns(sickAccrued);
            scheduledMock.Setup(x => x.GetHoursTaken(It.Is<LeaveType>(t => t == LeaveType.Sick), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(sickTaken);

            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, scheduledMock.Object, calculatorMock.Object, calendarMock.Object);

            LeaveSummary summary = sut.GetSickLeaveSummary(DateTime.Now);

            Assert.NotNull(summary);
            Assert.Equal(0, summary.BeginningBalance);
            Assert.Equal(sickAccrued, summary.Earned);
            Assert.Equal(LeaveType.Sick, summary.Type);
            Assert.Equal(sickTaken, summary.Used);
            Assert.Equal(sickBalance, summary.EndingBalance);
        }

        [Fact]
        public void LeaveSummaryService_TimeOff_Success()
        {
            const int toffStart = 24;
            const int toffTaken = 16;
            const int toffBalance = toffStart - toffTaken;
            settingsMock.Setup(x => x.TimeOffStart).Returns(toffStart);
            scheduledMock.Setup(x => x.GetHoursTaken(It.Is<LeaveType>(t => t == LeaveType.Timeoff), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(toffTaken);
            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, scheduledMock.Object, calculatorMock.Object, calendarMock.Object);

            LeaveSummary summary = sut.GetTimeOffAwardSummary(DateTime.Now);

            Assert.NotNull(summary);
            Assert.Equal(toffStart, summary.BeginningBalance);
            Assert.Equal(0, summary.Earned);
            Assert.Equal(LeaveType.Timeoff, summary.Type);
            Assert.Equal(toffTaken, summary.Used);
            Assert.Equal(toffBalance, summary.EndingBalance);
        }

        [Fact]
        public void LeaveSummaryService_UseOrLose_Success()
        {
            const int annualStart = 240;
            const int annualAccrued = 40;
            const int annualTaken = 32;
            const int useOrLose = (annualStart + annualAccrued - annualTaken) - Constants.MaxLeaveBalance;
            ICalendar outCalendar = new FederalPayCalendar(new DateTime(2022, 01, 02));

            settingsMock.Setup(x => x.AnnualLeaveStart).Returns(annualStart);
            calculatorMock.Setup(x => x.EndingLeaveBalance(It.IsAny<int>())).Returns(annualAccrued);
            scheduledMock.Setup(x => x.GetHoursTaken(It.Is<LeaveType>(t => t == LeaveType.Annual), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(annualTaken);
            calendarMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, scheduledMock.Object, calculatorMock.Object, calendarMock.Object);

            LeaveSummary summary = sut.GetUseOrLoseSummary(DateTime.Now);

            Assert.NotNull(summary);
            Assert.Equal(0, summary.BeginningBalance);
            Assert.Equal(0, summary.Earned);
            Assert.Equal(LeaveType.Annual, summary.Type);
            Assert.Equal(0, summary.Used);
            Assert.Equal(useOrLose, summary.EndingBalance);
        }
    }
}