using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.ViewModels;
using Moq;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class LeaveInformationViewModelTests
    {
        private readonly IFixture fixture;
        private readonly Mock<ILeaveSummaryService> summaryServiceMock;
        private readonly Mock<IFederalCalendarService> calendarServiceMock;
        private readonly Mock<INavigationService> navigationMock;
        private readonly DateTime calendarStart;
        private readonly Mock<ICalendar> calendar;

        public LeaveInformationViewModelTests()
        {
            fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            summaryServiceMock = new Mock<ILeaveSummaryService>();
            calendarServiceMock = new Mock<IFederalCalendarService>();
            navigationMock = new Mock<INavigationService>();
            calendarStart = new DateTime(2022, 01, 02);
            calendar = new Mock<ICalendar>();
        }
        [Fact]
        public void ViewModel_Initializes_FirstDayOfPayYear_Success()
        {
            DateTime expected = calendarStart;
            FederalPayPeriod payPeriod = new FederalPayPeriod(expected, 1);
            calendar.Setup(x => x.TryGetPayPeriodForDate(It.IsAny<DateTime>(), out payPeriod)).Returns(true);
            ICalendar outCalendar = calendar.Object;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, fixture.Create<ILeaveSummaryService>(),
                fixture.Create<INavigationService>());

            DateTime actual = sut.FirstDayOfPayYear;

            Assert.Equal(expected, actual);
        }
    }
}
