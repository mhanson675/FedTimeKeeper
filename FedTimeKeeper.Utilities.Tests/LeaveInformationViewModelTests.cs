using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.ViewModels;
using Moq;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class LeaveInformationViewModelTests
    {
        private readonly Mock<ILeaveSummaryService> summaryServiceMock;
        private readonly Mock<IFederalCalendarService> calendarServiceMock;
        private readonly Mock<INavigationService> navigationMock;
        private readonly Mock<FederalPayCalendar> calendar;

        public LeaveInformationViewModelTests()
        {
            summaryServiceMock = new Mock<ILeaveSummaryService>();
            calendarServiceMock = new Mock<IFederalCalendarService>();
            navigationMock = new Mock<INavigationService>();
            calendar = new Mock<FederalPayCalendar>();
        }
        [Fact]
        public void ViewModel_Initializes_FirstDayOfPayYear_Success()
        {
            DateTime expected = DateTime.Now;
            calendar.Setup(x => x.StartDate).Returns(expected);
            FederalPayCalendar outCalendar = calendar.Object;
            calendarServiceMock.Setup(x => x.TryGetPayCalendarForDate(It.IsAny<DateTime>(), out outCalendar)).Returns(true);
            LeaveInformationViewModel sut = new LeaveInformationViewModel(calendarServiceMock.Object, summaryServiceMock.Object,
                navigationMock.Object);

            DateTime actual = sut.FirstDayOfPayYear;

            Assert.Equal(expected, actual);
        }
    }
}
