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
        private readonly Mock<ISettingsService> settingsMock;
        private readonly DateTime calendarStartDate;
        private ICalendar outCalendar;
        private FederalPayPeriod outPayPeriod;

        public LeaveInformationViewModelTests()
        {
            fixture = new Fixture()
                .Customize(new AutoMoqCustomization());

            settingsMock = new Mock<ISettingsService>();
            settingsMock.Setup(x => x.SettingsDate).Returns(Constants.DefaultDate);

            summaryServiceMock = new Mock<ILeaveSummaryService>();
            calendarServiceMock = new Mock<IFederalCalendarService>();
            navigationMock = new Mock<INavigationService>();
            calendarStartDate = DateTime.Now.GetFirstSundayOfYear();
            outCalendar = new FederalPayCalendar(calendarStartDate);
            outPayPeriod = new FederalPayPeriod(calendarStartDate, 1);
        }
        
        [Fact]
        public void ViewModel_Initializes_FirstDayOfPayYear_Success()
        {
            DateTime expected = calendarStartDate;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out outPayPeriod))
                .Returns(true);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, fixture.Create<ILeaveSummaryService>(),
                fixture.Create<INavigationService>(), settingsMock.Object);

            DateTime actual = sut.FirstCalendarDate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ViewModel_Initializes_AsOfDate_Success()
        {
            DateTime start = calendarStartDate;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out outPayPeriod))
                .Returns(true);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, fixture.Create<ILeaveSummaryService>(),
                fixture.Create<INavigationService>(), settingsMock.Object);

            DateTime actual = sut.AsOfDate;

            Assert.Equal(DateTime.Now.Date, actual.Date);
        }

        [Fact]
        public void ViewModel_Initializes_ReportPeriodEndDate_Success()
        {
            DateTime start = calendarStartDate;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out outPayPeriod))
                .Returns(true);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, fixture.Create<ILeaveSummaryService>(),
                fixture.Create<INavigationService>(), settingsMock.Object);

            DateTime actual = sut.ReportPayPeriodEndDate;

            Assert.Equal(outPayPeriod.EndDate.Date, actual.Date);
        }

        [Fact]
        public void ViewModel_Initializes_LeaveSummary_Annual_Success()
        {
            LeaveSummary expected = new LeaveSummary
                {BeginningBalance = 40, Earned = 16, Type = LeaveType.Annual, Used = 0};
            DateTime start = calendarStartDate;
            FederalPayPeriod payPeriod = new FederalPayPeriod(start, 1);
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out payPeriod))
                .Returns(true);
            summaryServiceMock.Setup(x => x.GetAnnualLeaveSummary(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(expected);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, summaryServiceMock.Object,
                fixture.Create<INavigationService>(), settingsMock.Object);

            LeaveSummary actual = sut.Annual;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ViewModel_Initializes_LeaveSummary_Sick_Success()
        {
            LeaveSummary expected = new LeaveSummary
                { BeginningBalance = 40, Earned = 16, Type = LeaveType.Sick, Used = 0 };
            DateTime start = calendarStartDate;
            FederalPayPeriod payPeriod = new FederalPayPeriod(start, 1);
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out payPeriod))
                .Returns(true);
            summaryServiceMock.Setup(x => x.GetSickLeaveSummary(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(expected);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, summaryServiceMock.Object,
                fixture.Create<INavigationService>(), settingsMock.Object);

            LeaveSummary actual = sut.Sick;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ViewModel_Initializes_LeaveSummary_Annual_Fails_No_Calendar()
        {
            LeaveSummary expected = new LeaveSummary();
            DateTime start = calendarStartDate;
            FederalPayPeriod payPeriod = new FederalPayPeriod(start, 1);
            outCalendar = null;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(false);
            calendarServiceMock.Setup(x => x.TryGetPreviousPayPeriod(It.IsAny<DateTime>(), out payPeriod))
                .Returns(true);
            navigationMock.Setup(x =>
                x.DisplayAlertMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
            
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object,
                fixture.Create<ILeaveSummaryService>(), navigationMock.Object, settingsMock.Object);

            LeaveSummary actual = sut.Annual;

            Assert.Equal(expected.BeginningBalance, actual.BeginningBalance);
            Assert.Equal(expected.EndingBalance, actual.EndingBalance);
            Assert.Equal(expected.Earned, actual.Earned);
            Assert.Equal(expected.Used, actual.Used);

            navigationMock.Verify(x => x.DisplayAlertMessage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }
    }
}
