using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FedTimeKeeper.Services;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class FedCalendarServiceTests
    {
        private readonly FederalCalendarService sut;

        public FedCalendarServiceTests()
        {
            sut = new FederalCalendarService();
        }
        [Fact]
        public void CalendarServcie_Initializes_With_Calendars()
        {
            Assert.NotNull(sut.PayCalendars);
            Assert.NotEmpty(sut.PayCalendars);
            Assert.Equal(5, sut.PayCalendars.Count);
        }

        [Theory]
        [MemberData(nameof(GetTryGetPayCalendarData))]
        public void CalendarService_TryGetPayCalendar_Success(FederalPayCalendar expectedCalendar, DateTime tryDate)
        {
            _ = sut.TryGetPayCalendarForDate(tryDate, out FederalPayCalendar actualCalendar);

            Assert.NotNull(actualCalendar);
            Assert.Equal(expectedCalendar.StartDate, actualCalendar.StartDate);
            Assert.Equal(expectedCalendar.EndDate, actualCalendar.EndDate);
            Assert.Equal(expectedCalendar.PayYear, actualCalendar.PayYear);
            Assert.Equal(expectedCalendar.NumberOfPayPeriods, actualCalendar.NumberOfPayPeriods);
        }

        [Fact]
        public void CalendarService_TryGetPayCalendar_Fails()
        {
            DateTime tryDate = new DateTime(2022, 01, 18);
            bool actual = sut.TryGetPayCalendarForDate(tryDate, out _);

            Assert.False(actual);
        }

        public static TheoryData<FederalPayCalendar, DateTime> GetTryGetPayCalendarData()
        {
            FederalPayCalendar cy22 = new FederalPayCalendar(new DateTime(2022, 01, 02));
            FederalPayCalendar cy23 = new FederalPayCalendar(new DateTime(2023, 01, 01));
            FederalPayCalendar cy24 = new FederalPayCalendar(new DateTime(2024, 01, 14));

            TheoryData<FederalPayCalendar, DateTime> data = new TheoryData<FederalPayCalendar, DateTime>
            {
                {cy22, new DateTime(2022, 01, 02)},
                {cy22, new DateTime(2022, 01, 06)},
                {cy22, new DateTime(2022, 11, 06)},
                {cy23, new DateTime(2023, 01, 06)},
                {cy23, new DateTime(2023, 08, 06)},
                {cy23, new DateTime(2023, 11, 06)},
                {cy24, new DateTime(2024, 01, 20)},
                {cy24, new DateTime(2024, 07, 06)},
                {cy24, new DateTime(2024, 10, 06)}
            };

            return data;
        }
    }
}
