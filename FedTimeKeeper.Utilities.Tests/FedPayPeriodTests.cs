using System;
using System.Collections.Generic;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class FedPayPeriodTests
    {
        private readonly DateTime testStart;
        private readonly FederalPayPeriod testPeriod;

        public FedPayPeriodTests()
        {
            testStart = new DateTime(2022, 01, 02);
            testPeriod = new FederalPayPeriod(testStart, 1);
        }

        #region Instantiation
        [Theory]
        [MemberData(nameof(GetInvalidStartDateData))]
        public void PayPeriod_Fails_Invalid_StartDate(DateTime startDate, DayOfWeek dayOfWeek)
        {
            ArgumentOutOfRangeException error = Assert.Throws<ArgumentOutOfRangeException>(() => new FederalPayPeriod(startDate, 1));

            Assert.Equal(error.ActualValue, dayOfWeek);
        }

        [Theory]
        [MemberData(nameof(InvalidPeriodNumberData))]
        public void PayPeriod_Fails_Invalid_Period(int payPeriod)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new FederalPayPeriod(testStart, payPeriod));
        }

        [Theory]
        [MemberData(nameof(SuccessData))]
        public void PayPeriod_Success(DateTime expectedStart, DateTime expectedEnd)
        {
            FederalPayPeriod actual = new FederalPayPeriod(expectedStart, 1);

            Assert.Equal(expectedStart, actual.StartDate);
            Assert.Equal(expectedEnd, actual.EndDate);
        }
        #endregion

        #region Methods
        [Theory]
        [MemberData(nameof(GetIncludesDateData))]
        public void PayPeriod_IncludesDate_Returns_Expected(DateTime date, bool expected)
        {
            bool actual = testPeriod.IncludesDate(date);

            Assert.Equal(expected, actual);
        }
        #endregion

        #region DataProviders
        public static TheoryData<DateTime, DayOfWeek> GetInvalidStartDateData()
        {
            DateTime start = new DateTime(2022, 01, 02);
            TheoryData<DateTime, DayOfWeek> data = new TheoryData<DateTime, DayOfWeek>
            {
                { start.AddDays(1), start.AddDays(1).DayOfWeek },
                { start.AddDays(3), start.AddDays(3).DayOfWeek },
                { start.AddDays(5), start.AddDays(5).DayOfWeek },
                { start.AddDays(10), start.AddDays(10).DayOfWeek }
            };

            return data;
        }
        public static TheoryData<int> InvalidPeriodNumberData => new TheoryData<int>
        {
            {0},
            {-1},
            {28}
        };
        public static TheoryData<DateTime, DateTime> SuccessData => new TheoryData<DateTime, DateTime>
        {
            {DateTime.Parse("2022-01-02"), DateTime.Parse("2022-01-15")},
            {DateTime.Parse("2022-01-16"), DateTime.Parse("2022-01-29")},
            {DateTime.Parse("2022-01-30"), DateTime.Parse("2022-02-12")},
            {DateTime.Parse("2022-02-13"), DateTime.Parse("2022-02-26")},
            {DateTime.Parse("2022-02-27"), DateTime.Parse("2022-03-12")},
        };
        public static TheoryData<DateTime, bool> GetIncludesDateData()
        {
            DateTime start = new DateTime(2022, 01, 02);
            TheoryData<DateTime, bool> data = new TheoryData<DateTime, bool>
            {
                { start, true },
                { start.AddDays(3), true },
                { start.AddDays(5), true },
                { start.AddDays(10), true },
                { start.AddDays(13), true },
                { start.AddDays(14), false },
                { start.AddDays(16), false },
                { start.AddDays(-1), false }
            };

            return data;
        }
        #endregion
    }
}
