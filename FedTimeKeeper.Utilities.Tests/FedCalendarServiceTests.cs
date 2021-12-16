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

        #region Inintialization

        [Fact]
        public void CalendarServcie_Initializes_With_Calendars()
        {
            Assert.NotNull(sut.PayCalendars);
            Assert.NotEmpty(sut.PayCalendars);
            Assert.Equal(5, sut.PayCalendars.Count);
        }

        #endregion Inintialization

        #region Methods

        [Theory]
        [MemberData(nameof(GetTryGetPayCalendarSuccessData))]
        public void CalendarService_TryGetPayCalendar_Success(FederalPayCalendar expectedCalendar, DateTime tryDate)
        {
            _ = sut.TryGetPayCalendarForDate(tryDate, out FederalPayCalendar actualCalendar);

            Assert.NotNull(actualCalendar);
            Assert.Equal(expectedCalendar.StartDate, actualCalendar.StartDate);
            Assert.Equal(expectedCalendar.EndDate, actualCalendar.EndDate);
            Assert.Equal(expectedCalendar.PayYear, actualCalendar.PayYear);
            Assert.Equal(expectedCalendar.NumberOfPayPeriods, actualCalendar.NumberOfPayPeriods);
        }

        [Theory]
        [MemberData(nameof(TryGetFailDates))]
        public void CalendarService_TryGetPayCalendar_Fails(DateTime tryDate)
        {
            bool actual = sut.TryGetPayCalendarForDate(tryDate, out _);

            Assert.False(actual);
        }

        [Theory]
        [MemberData(nameof(GetTryGetPayPeroidSuccessData))]
        public void CalendarService_TryGetPayPeriodForDate_Success(FederalPayPeriod expected, DateTime tryDate)
        {
            _ = sut.TryGetPayPeriodForDate(tryDate, out FederalPayPeriod actual);

            Assert.Equal(expected.StartDate, actual.StartDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Period, actual.Period);
        }

        [Theory]
        [MemberData(nameof(TryGetFailDates))]
        public void CalendarService_TryGetPayPeriodForDate_Fails(DateTime tryDate)
        {
            bool actual = sut.TryGetPayPeriodForDate(tryDate, out _);

            Assert.False(actual);
        }

        [Theory]
        [MemberData(nameof(GetTryGetPreviousPayPeroidSuccessData))]
        public void CalendarService_TryGetPreviousPayPeriod_Success(FederalPayPeriod expected, DateTime tryDate)
        {
            _ = sut.TryGetPreviousPayPeriod(tryDate, out FederalPayPeriod actual);

            Assert.Equal(expected.StartDate, actual.StartDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Period, actual.Period);
        }

        [Theory]
        [MemberData(nameof(TryGetFailDates))]
        public void CalendarService_TryGetPrevioudPayPeriod_Fails(DateTime tryDate)
        {
            bool actual = sut.TryGetPreviousPayPeriod(tryDate, out _);

            Assert.False(actual);
        }

        #endregion Methods

        #region Data Providers

        public static TheoryData<FederalPayCalendar, DateTime> GetTryGetPayCalendarSuccessData()
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

        public static TheoryData<DateTime> TryGetFailDates => new TheoryData<DateTime>
        {
            { new DateTime(2021,12,31) },
            { new DateTime(2020,12,31) },
            { new DateTime(2031,01,01) },
            { new DateTime(2019,12,23) },
            { new DateTime(2027,01,10) }
        };

        public static TheoryData<FederalPayPeriod, DateTime> GetTryGetPayPeroidSuccessData()
        {
            FederalPayPeriod period1 = new FederalPayPeriod(new DateTime(2022, 02, 13), 04);
            FederalPayPeriod period2 = new FederalPayPeriod(new DateTime(2023, 07, 02), 14);
            FederalPayPeriod period3 = new FederalPayPeriod(new DateTime(2024, 11, 17), 23);
            FederalPayPeriod period4 = new FederalPayPeriod(new DateTime(2025, 05, 04), 09);
            FederalPayPeriod period5 = new FederalPayPeriod(new DateTime(2026, 12, 27), 26);

            TheoryData<FederalPayPeriod, DateTime> data = new TheoryData<FederalPayPeriod, DateTime>
            {
                { period1, new DateTime(2022, 02, 16) },
                { period2, new DateTime(2023, 07, 13) },
                { period3, new DateTime(2024, 11, 21) },
                { period4, new DateTime(2025, 05, 14) },
                { period5, new DateTime(2026, 12, 27) }
            };

            return data;
        }

        public static TheoryData<FederalPayPeriod, DateTime> GetTryGetPreviousPayPeroidSuccessData()
        {
            FederalPayPeriod period1 = new FederalPayPeriod(new DateTime(2022, 02, 13), 04);
            FederalPayPeriod period2 = new FederalPayPeriod(new DateTime(2022, 12, 18), 26);
            FederalPayPeriod period3 = new FederalPayPeriod(new DateTime(2023, 07, 02), 14);
            FederalPayPeriod period4 = new FederalPayPeriod(new DateTime(2023, 12, 31), 27);
            FederalPayPeriod period5 = new FederalPayPeriod(new DateTime(2024, 11, 17), 23);
            FederalPayPeriod period6 = new FederalPayPeriod(new DateTime(2024, 12, 29), 26);
            FederalPayPeriod period7 = new FederalPayPeriod(new DateTime(2025, 05, 04), 09);
            FederalPayPeriod period8 = new FederalPayPeriod(new DateTime(2025, 12, 28), 26);
            FederalPayPeriod period9 = new FederalPayPeriod(new DateTime(2026, 12, 13), 25);

            TheoryData<FederalPayPeriod, DateTime> data = new TheoryData<FederalPayPeriod, DateTime>
            {
                { period1, new DateTime(2022, 02, 28) },
                { period2, new DateTime(2023, 01, 01) },
                { period3, new DateTime(2023, 07, 19) },
                { period4, new DateTime(2024, 01, 17) },
                { period5, new DateTime(2024, 12, 07) },
                { period6, new DateTime(2025, 01, 12) },
                { period7, new DateTime(2025, 05, 21) },
                { period8, new DateTime(2026, 01, 23) },
                { period9, new DateTime(2026, 12, 27) }
            };

            return data;
        }

        #endregion Data Providers
    }
}