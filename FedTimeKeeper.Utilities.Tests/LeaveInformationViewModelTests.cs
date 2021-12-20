using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FedTimeKeeper.Models;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities.Tests.HelperExtensions;
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
            calendar.Setup(x => x.StartDate).Returns(expected);
            ICalendar outCalendar = calendar.Object;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out payPeriod))
                .Returns(true);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, fixture.Create<ILeaveSummaryService>(),
                fixture.Create<INavigationService>());

            DateTime actual = sut.FirstDayOfPayYear;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ViewModel_Initializes_AsOfDate_Success()
        {
            DateTime start = DateTime.Now.GetLastSunday();
            FederalPayPeriod payPeriod = new FederalPayPeriod(start, 1);
            calendar.Setup(x => x.StartDate).Returns(start);
            ICalendar outCalendar = calendar.Object;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out payPeriod))
                .Returns(true);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, fixture.Create<ILeaveSummaryService>(),
                fixture.Create<INavigationService>());

            DateTime actual = sut.AsOfDate;

            Assert.Equal(DateTime.Now.Date, actual.Date);
        }

        [Fact]
        public void ViewModel_Initializes_ReportPeriodEndDate_Success()
        {
            DateTime start = DateTime.Now.GetLastSunday();
            FederalPayPeriod payPeriod = new FederalPayPeriod(start, 1);
            calendar.Setup(x => x.StartDate).Returns(start);
            ICalendar outCalendar = calendar.Object;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out payPeriod))
                .Returns(true);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, fixture.Create<ILeaveSummaryService>(),
                fixture.Create<INavigationService>());

            DateTime actual = sut.ReportPayPeriodEndDate;

            Assert.Equal(payPeriod.EndDate.Date, actual.Date);
        }

        [Fact]
        public void ViewModel_Initializes_LeaveSummary_Annual_Success()
        {
            LeaveSummary expected = new LeaveSummary
                {BeginningBalance = 40, Earned = 16, Type = LeaveType.Annual, Used = 0};
            DateTime start = DateTime.Now.GetLastSunday();
            FederalPayPeriod payPeriod = new FederalPayPeriod(start, 1);
            calendar.Setup(x => x.StartDate).Returns(start);
            ICalendar outCalendar = calendar.Object;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out payPeriod))
                .Returns(true);
            summaryServiceMock.Setup(x => x.GetAnnualLeaveSummary(It.IsAny<DateTime>())).Returns(expected);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, summaryServiceMock.Object,
                fixture.Create<INavigationService>());

            LeaveSummary actual = sut.Annual;

            Assert.Equal(expected, actual);
        }
    }
}
