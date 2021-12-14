using System;
using System.Collections.Generic;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class FedPayPeriodTests
    {
        private readonly DateTime standardStart;
        private readonly FederalPayPeriod sut;

        public FedPayPeriodTests()
        {
            standardStart = new DateTime(2022, 01, 02);
            sut = new FederalPayPeriod(standardStart, 1);
        }
        [Theory]
        [MemberData(nameof(GetInvalidDateRanges))]
        public void PayPeriod_Fails_Invalid_StartDate(DateTime startDate, DayOfWeek dayOfWeek)
        {
            ArgumentOutOfRangeException error = Assert.Throws<ArgumentOutOfRangeException>(() => new FederalPayPeriod(startDate, 1));

            Assert.Equal(error.ActualValue, dayOfWeek);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(28)]
        public void PayPeriod_Fails_Invalid_Period(int payPeriod)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new FederalPayPeriod(standardStart, payPeriod));
        }

        [Theory]
        [MemberData(nameof(GetIncludesDateData))]
        public void PayPeriod_IncludesDate_Returns_Expected(DateTime date, bool expected)
        {
            bool actual = sut.IncludesDate(date);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PayPeriod_EndDate_Correct()
        {
            DateTime expected = new DateTime(2022, 01, 15);
            Assert.Equal(expected, sut.EndDate);
        }

        public static IEnumerable<object[]> GetInvalidDateRanges()
        {
            DateTime start = new DateTime(2022, 01, 02);
            yield return new object[] { start.AddDays(1), start.AddDays(1).DayOfWeek };
            yield return new object[] { start.AddDays(3), start.AddDays(3).DayOfWeek };
            yield return new object[] { start.AddDays(5), start.AddDays(5).DayOfWeek };
            yield return new object[] { start.AddDays(10), start.AddDays(10).DayOfWeek };
        }

        public static IEnumerable<object[]> GetIncludesDateData()
        {
            DateTime start = new DateTime(2022, 01, 02);
            yield return new object[] { start, true };
            yield return new object[] { start.AddDays(3), true };
            yield return new object[] { start.AddDays(5), true };
            yield return new object[] { start.AddDays(10), true };
            yield return new object[] { start.AddDays(13), true };
            yield return new object[] { start.AddDays(14), false };
            yield return new object[] { start.AddDays(16), false };
            yield return new object[] { start.Subtract(TimeSpan.FromDays(1)), false };
        }
    }
}
