using System;
using System.Collections.Generic;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class FedPayCalendarTests
    {
        private readonly DateTime standardStartDate;
        private readonly DateTime standardEndDate;
        private readonly DateTime eighthPayPeriodStartDate;
        private readonly DateTime eighthPayPeriodSampleDate;
        private readonly FederalPayCalendar sut;

        public FedPayCalendarTests()
        {
            standardStartDate = new DateTime(2022, 01, 02);
            standardEndDate = new DateTime(2022, 12, 31);
            sut = new FederalPayCalendar(standardStartDate, standardEndDate);
            eighthPayPeriodSampleDate = new DateTime(2021, 04, 15);
            eighthPayPeriodStartDate = new DateTime(2021, 04, 10);
            
        }

        [Theory]
        [MemberData(nameof(GetInvalidDateRanges))]
        public void PayCalendar_Fails_Invalid_DateRange(DateTime start, DateTime end)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new FederalPayCalendar(start, end));
        }

        [Theory]
        [InlineData("2022-01-03", DayOfWeek.Monday)]
        [InlineData("2022-01-04", DayOfWeek.Tuesday)]
        [InlineData("2022-01-05", DayOfWeek.Wednesday)]
        [InlineData("2022-01-06", DayOfWeek.Thursday)]
        [InlineData("2022-01-07", DayOfWeek.Friday)]
        [InlineData("2022-01-08", DayOfWeek.Saturday)]
        public void PayCalendar_Fails_StartDate_Not_Sunday(DateTime start, DayOfWeek expected)
        {
            ArgumentOutOfRangeException result = Assert.Throws<ArgumentOutOfRangeException>(() => new FederalPayCalendar(start, standardEndDate));

            Assert.Equal(expected, result.ActualValue);
        }

        [Theory]
        [InlineData("2022-01-09", DayOfWeek.Sunday)]
        [InlineData("2022-01-10", DayOfWeek.Monday)]
        [InlineData("2022-01-11", DayOfWeek.Tuesday)]
        [InlineData("2022-01-12", DayOfWeek.Wednesday)]
        [InlineData("2022-01-13", DayOfWeek.Thursday)]
        [InlineData("2022-01-14", DayOfWeek.Friday)]
        public void PayCalendar_Fails_EndDate_Not_Saturday(DateTime end, DayOfWeek expected)
        {
            ArgumentOutOfRangeException result = Assert.Throws<ArgumentOutOfRangeException>(() => new FederalPayCalendar(standardStartDate, end));

            Assert.Equal(expected, result.ActualValue);
        }

        [Fact]
        public void PayCalendar_Populates_PayPeriods_Success()
        {
            int expected = 26;

            Assert.NotEmpty(sut.PayPeriods);
            Assert.Equal(expected, sut.NumberOfPayPeriods);
        }

        [Fact]
        public void ReturnsCorrectFirstPayPeriodOfYear()
        {
            FederalPayCalendar payCalendar = new FederalPayCalendar(standardStartDate, standardEndDate);
            FederalPayPeriod expected = new FederalPayPeriod(standardStartDate, 1);
            FederalPayPeriod actual = payCalendar.GetFirstPayPeriod();

            Assert.Equal(expected.StartDate, actual.StartDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Period, actual.Period);
        }

        [Fact]
        public void ReturnsCorrectLastPayPeriodOfYear()
        {
            FederalPayCalendar payCalendar = new FederalPayCalendar(standardStartDate, standardEndDate);
            DateTime lastPeriodStart = new DateTime(2021, 12, 19);
            FederalPayPeriod expected = new FederalPayPeriod(lastPeriodStart, 26);
            FederalPayPeriod actual = payCalendar.GetFinalPayPeriod();

            Assert.Equal(expected.StartDate, actual.StartDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Period, actual.Period);
        }

        [Fact]
        public void ReturnsCorrectCurrentPayPeriod()
        {
            FederalPayCalendar payCalendar = new FederalPayCalendar(standardStartDate, standardEndDate);
            FederalPayPeriod expected = new FederalPayPeriod(eighthPayPeriodStartDate, 8);
            bool found = payCalendar.TryGetPayPeriodForDate(eighthPayPeriodSampleDate, out FederalPayPeriod actual);

            Assert.True(found);
            Assert.Equal(expected.StartDate, actual.StartDate);
            Assert.Equal(expected.EndDate, actual.EndDate);
            Assert.Equal(expected.Period, actual.Period);
        }

        public static IEnumerable<object[]> GetInvalidDateRanges()
        {
            DateTime start = new DateTime(2022, 01, 02);
            yield return new object[] { start, start };
            yield return new object[] { start, start.Subtract(TimeSpan.FromDays(1)) };
        }
    }
}