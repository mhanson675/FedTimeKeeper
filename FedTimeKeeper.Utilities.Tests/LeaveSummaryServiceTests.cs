using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FedTimeKeeper.Models;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using Moq;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class LeaveSummaryServiceTests
    {
        private readonly IFixture fixture;
        private readonly Mock<ISettingsService> settingsMock;
        private readonly Mock<IScheduledLeaveService> scheduleMock;
        private readonly Mock<IFederalLeaveCalculator> calculatorMock;
        private readonly Mock<IFederalCalendarService> calendarMock;
        private readonly Mock<IScheduledLeaveService> leaveMock;

        public LeaveSummaryServiceTests()
        {
            fixture = new Fixture();

            settingsMock = new Mock<ISettingsService>();
            settingsMock.Setup(x => x.AccrualRate).Returns(8);
            settingsMock.Setup(x => x.AnnualLeaveStart).Returns(0);
            settingsMock.Setup(x => x.SickLeaveStart).Returns(0);
            settingsMock.Setup(x => x.TimeOffStart).Returns(0);

            leaveMock = new Mock<IScheduledLeaveService>();

            calculatorMock = new Mock<IFederalLeaveCalculator>();
            calendarMock = new Mock<IFederalCalendarService>();
        }

        [Fact]
        public void LeaveSummaryService_Success()
        {
            DateTime twoJan = new DateTime(2022, 01, 02);
            FederalPayPeriod payPeriod = new FederalPayPeriod(twoJan, 10);
            ScheduledLeave leave = new ScheduledLeave()
            {
                StartDate = twoJan,
                EndDate = twoJan,
                HoursTaken = 8,
                Id = 0,
                Type = LeaveType.Annual
            };

            leaveMock.Setup(x => x.GetAllScheduled()).Returns(new List<ScheduledLeave> {leave});
            calendarMock.Setup(x => x.TryGetPayPeriodForDate(It.IsAny<DateTime>(), out payPeriod)).Returns(true);

            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, leaveMock.Object, calculatorMock.Object, calendarMock.Object);

            var summary = sut.GetAnnualLeaveSummary(DateTime.Now);

            Assert.NotNull(summary);
        }
    }
}
