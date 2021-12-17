using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using Moq;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class LeaveSummaryServiceTests
    {
        private readonly Mock<ISettingsService> settingsMock;
        private readonly Mock<IScheduledLeaveService> scheduleMock;
        private readonly Mock<IFederalLeaveCalculator> calculatorMock;
        private readonly Mock<IFederalCalendarService> calendarMock;
        private readonly IScheduledLeaveService leaveService;

        public LeaveSummaryServiceTests()
        {
            IFixture fixture = new Fixture();
            
            settingsMock = new Mock<ISettingsService>();
            settingsMock.Setup(x => x.AccrualRate).Returns(8);
            settingsMock.Setup(x => x.AnnualLeaveStart).Returns(0);
            settingsMock.Setup(x => x.SickLeaveStart).Returns(0);
            settingsMock.Setup(x => x.TimeOffStart).Returns(0);

            leaveService = fixture.Create<IScheduledLeaveService>();

            calculatorMock = new Mock<IFederalLeaveCalculator>();
            calendarMock = new Mock<IFederalCalendarService>();
        }

        [Fact]
        public void LeaveSummaryService_Success()
        {
            ILeaveSummaryService sut = new LeaveSummaryService(settingsMock.Object, leaveService, calculatorMock.Object, calendarMock.Object);

        }
    }
}
