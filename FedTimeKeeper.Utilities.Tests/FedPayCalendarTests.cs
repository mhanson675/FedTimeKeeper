using System;
using System.Collections.Generic;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class FedPayCalendarTests
    {
        private readonly DateTime testStartDate;
        private readonly FederalPayCalendar testCalendar;

        public FedPayCalendarTests()
        {
            testStartDate = new DateTime(2022, 01, 02);
            testCalendar = new FederalPayCalendar(testStartDate);
        }

        #region Instantiation
        [Theory]
        [MemberData(nameof(StartDateNotSundayData))]
        public void PayCalendar_Fails_StartDate_Not_Sunday(DateTime start, DayOfWeek expected)
        {
            ArgumentOutOfRangeException result = Assert.Throws<ArgumentOutOfRangeException>(() => new FederalPayCalendar(start));

            Assert.Equal(expected, result.ActualValue);
        }

        [Theory]
        [MemberData(nameof(StartDateMonthNotJanuaryData))]
        public void PayCalendar_Fails_StartDate_Month_Not_January(DateTime start, int expected)
        {
            ArgumentOutOfRangeException result = Assert.Throws<ArgumentOutOfRangeException>(() => new FederalPayCalendar(start));

            Assert.Equal(expected, result.ActualValue);
        }

        [Theory]
        [MemberData(nameof(PayCalendarData))]
        public void PayCalendar_Populates_PayPeriods_Success(DateTime start, DateTime unUsed, int expected)
        {
            FederalPayCalendar actual = new FederalPayCalendar(start);

            Assert.Equal(expected, actual.NumberOfPayPeriods);
            Assert.Equal(start, actual.StartDate);
            Assert.Equal(unUsed, actual.EndDate);
            Assert.Equal(start.Year, actual.PayYear);
        }
        #endregion

        #region Methods
        [Theory]
        [MemberData(nameof(IncludesDateSuccessData))]
        public void PayCalendar_IncludesDate_Returns_Success(DateTime date, bool expected)
        {
            bool actual = testCalendar.IncludesDate(date);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PayCalendar_GetFirstPayPeriod_Success()
        {
            FederalPayPeriod expected = new FederalPayPeriod(testStartDate, 1);

            FederalPayPeriod actual = testCalendar.GetFirstPayPeriod();

            Assert.Equal(expected.StartDate, actual.StartDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Period, actual.Period);
        }

        [Fact]
        public void PayCalendar_GetFinalPayPeriod_Success()
        {
            FederalPayPeriod expected = new FederalPayPeriod(DateTime.Parse("2022-12-18"), 26);
            FederalPayPeriod actual = testCalendar.GetFinalPayPeriod();

            Assert.Equal(expected.StartDate, actual.StartDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Period, actual.Period);
        }

        [Theory]
        [MemberData(nameof(TryGetCurrentPayPeriodForDateSuccessData))]
        public void PayCalendar_TryGetPayPeriodForDate_Success(DateTime tryDate, int expectedPeriod, DateTime expectedStart)
        {
            FederalPayPeriod expected = new FederalPayPeriod(expectedStart, expectedPeriod);
            bool found = testCalendar.TryGetPayPeriodForDate(tryDate, out FederalPayPeriod actual);

            Assert.True(found);
            Assert.Equal(expected.StartDate, actual.StartDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Period, actual.Period);
        }

        [Theory]
        [MemberData(nameof(TryGetPayPeriodForDateFailData))]
        public void PayCalendar_TryGetPayPeriodForDate_Fails(DateTime tryDate)
        {
            bool actual = testCalendar.TryGetPayPeriodForDate(tryDate, out _);

            Assert.False(actual);
        }

        [Theory]
        [MemberData(nameof(TryGetPreviousPayPeriodSuccessData))]
        public void PayCalendar_TryGetPreviousPayPeriod_Success(DateTime tryDate, int expectedPeriod, DateTime expectedStart)
        {
            FederalPayPeriod expected = new FederalPayPeriod(expectedStart, expectedPeriod);
            bool found = testCalendar.TryGetPreviousPayPeriod(tryDate, out FederalPayPeriod actual);

            Assert.True(found);
            Assert.Equal(expected.StartDate, actual.StartDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Period, actual.Period);
        }

        [Theory]
        [MemberData(nameof(TryGetPreviousPayPeriodFailData))]
        public void PayCalendar_TryGetPreviousPayPeriod_Fails(DateTime tryDate)
        {
            bool actual = testCalendar.TryGetPreviousPayPeriod(tryDate, out _);

            Assert.False(actual);
        }
        #endregion

        #region Data Providers
        public static TheoryData<DateTime, DayOfWeek> StartDateNotSundayData => new TheoryData<DateTime, DayOfWeek>
        {
            { DateTime.Parse("2022-01-03"), DayOfWeek.Monday },
            { DateTime.Parse("2022-01-04"), DayOfWeek.Tuesday },
            { DateTime.Parse("2022-01-05"), DayOfWeek.Wednesday },
            { DateTime.Parse("2022-01-06"), DayOfWeek.Thursday },
            { DateTime.Parse("2022-01-07"), DayOfWeek.Friday },
            { DateTime.Parse("2022-01-08"), DayOfWeek.Saturday }
        };
        public static TheoryData<DateTime, int> StartDateMonthNotJanuaryData => new TheoryData<DateTime, int>
        {
            { DateTime.Parse("2022-02-06"), 02 },
            { DateTime.Parse("2022-03-06"), 03 },
            { DateTime.Parse("2022-04-03"), 04 },
            { DateTime.Parse("2022-10-02"), 10 },
            { DateTime.Parse("2022-11-06"), 11 },
            { DateTime.Parse("2022-12-04"), 12 }
        };
        public static TheoryData<DateTime, DateTime, int> PayCalendarData =>
            new TheoryData<DateTime, DateTime, int>
            {
                { DateTime.Parse("2022-01-02"), DateTime.Parse("2022-12-31"), 26 },
                { DateTime.Parse("2023-01-01"), DateTime.Parse("2024-01-13"), 27 },
                { DateTime.Parse("2024-01-14"), DateTime.Parse("2025-01-11"), 26 },
                { DateTime.Parse("2025-01-12"), DateTime.Parse("2026-01-10"), 26 },
                { DateTime.Parse("2026-01-11"), DateTime.Parse("2027-01-09"), 26 },
                { DateTime.Parse("2034-01-01"), DateTime.Parse("2035-01-13"), 27 }
            };
        public static TheoryData<DateTime, bool> IncludesDateSuccessData => new TheoryData<DateTime, bool>
        {
            { DateTime.Parse("2022-01-02"), true },
            { DateTime.Parse("2022-01-03"), true },
            { DateTime.Parse("2022-04-02"), true },
            { DateTime.Parse("2022-06-02"), true },
            { DateTime.Parse("2022-12-02"), true },
            { DateTime.Parse("2022-12-31"), true },
            { DateTime.Parse("2021-12-31"), false },
            { DateTime.Parse("2021-01-02"), false },
            { DateTime.Parse("2023-01-03"), false }
        };
        public static TheoryData<DateTime, int, DateTime> TryGetCurrentPayPeriodForDateSuccessData =>
            new TheoryData<DateTime, int, DateTime>
            {
                { DateTime.Parse("2022-01-12"), 1, DateTime.Parse("2022-01-02") },
                { DateTime.Parse("2022-01-19"), 2, DateTime.Parse("2022-01-16") },
                { DateTime.Parse("2022-02-13"), 4, DateTime.Parse("2022-02-13") },
                { DateTime.Parse("2022-03-26"), 6, DateTime.Parse("2022-03-13") },
                { DateTime.Parse("2022-05-12"), 10, DateTime.Parse("2022-05-08") },
                { DateTime.Parse("2022-06-12"), 12, DateTime.Parse("2022-06-05") },
                { DateTime.Parse("2022-07-12"), 14, DateTime.Parse("2022-07-03") },
                { DateTime.Parse("2022-08-20"), 17, DateTime.Parse("2022-08-14") },
                { DateTime.Parse("2022-09-30"), 20, DateTime.Parse("2022-09-25") }
            };
        public static TheoryData<DateTime> TryGetPayPeriodForDateFailData => new TheoryData<DateTime>
        {
            DateTime.Parse("2021-12-31"),
            DateTime.Parse("2022-01-01"),
            DateTime.Parse("2023-01-01"),
        };
        public static TheoryData<DateTime, int, DateTime> TryGetPreviousPayPeriodSuccessData =>
            new TheoryData<DateTime, int, DateTime>
            {
                { DateTime.Parse("2022-01-16"), 1, DateTime.Parse("2022-01-02") },
                { DateTime.Parse("2022-01-30"), 2, DateTime.Parse("2022-01-16") },
                { DateTime.Parse("2022-02-27"), 4, DateTime.Parse("2022-02-13") },
                { DateTime.Parse("2022-03-31"), 6, DateTime.Parse("2022-03-13") },
                { DateTime.Parse("2022-06-04"), 10, DateTime.Parse("2022-05-08") },
                { DateTime.Parse("2022-06-19"), 12, DateTime.Parse("2022-06-05") },
                { DateTime.Parse("2022-07-30"), 14, DateTime.Parse("2022-07-03") },
                { DateTime.Parse("2022-08-28"), 17, DateTime.Parse("2022-08-14") },
                { DateTime.Parse("2022-10-10"), 20, DateTime.Parse("2022-09-25") }
            };
        public static TheoryData<DateTime> TryGetPreviousPayPeriodFailData => new TheoryData<DateTime>
        {
            DateTime.Parse("2021-12-31"),
            DateTime.Parse("2022-01-02"),
            DateTime.Parse("2023-01-01"),
        };
        #endregion
    }
}