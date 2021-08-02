using System;
using FedTimeKeeper.Utilities;
using Xunit;

namespace FedTimeKeeper.Utilities.Tests
{
    public class FedPayCalendarTests
    {
        private readonly DateTime firstPayPeriod;
        private readonly DateTime lastPayPeriod;
        private readonly DateTime eighthPayPeriodStartDate;
        private readonly DateTime eighthPayPeriodSampleDate;
        private readonly int payPeriodLength;
        private readonly int periodsInTestYear;

        public FedPayCalendarTests()
        {
            firstPayPeriod = new DateTime(2021, 01, 03);
            lastPayPeriod = new DateTime(2021, 12, 19);
            eighthPayPeriodSampleDate = new DateTime(2021, 04, 15);
            eighthPayPeriodStartDate = new DateTime(2021, 04, 11);
            payPeriodLength = 14;
            periodsInTestYear = 26;
        }

        [Fact]
        public void ReturnsCorrectFirstPayPeriodOfYear()
        {
            var payCalendar = new FederalPayCalendar(firstPayPeriod, lastPayPeriod);
            var expectedPayPeriod = new FederalPayPeriod(firstPayPeriod, firstPayPeriod.AddDays(payPeriodLength - 1), 1);
            var actualPPReturned = payCalendar.GetFirstPayPeriod();

            Assert.Equal(expectedPayPeriod.StartDate, actualPPReturned.StartDate);
            Assert.Equal(expectedPayPeriod.EndDate, actualPPReturned.EndDate);
            Assert.Equal(expectedPayPeriod.Period, actualPPReturned.Period);
        }

        [Fact]
        public void ReturnsCorrectLastPayPeriodOfYear()
        {
            var payCalendar = new FederalPayCalendar(firstPayPeriod, lastPayPeriod);
            var expectedPayPeriod = new FederalPayPeriod(lastPayPeriod, lastPayPeriod.AddDays(payPeriodLength - 1), periodsInTestYear);
            var actualPPReturned = payCalendar.GetLastPayPeriod();

            Assert.Equal(expectedPayPeriod.StartDate, actualPPReturned.StartDate);
            Assert.Equal(expectedPayPeriod.EndDate, actualPPReturned.EndDate);
            Assert.Equal(expectedPayPeriod.Period, actualPPReturned.Period);
        }

        [Fact]
        public void ReturnsCorrectCurrentPayPeriod()
        {
            var payCalendar = new FederalPayCalendar(firstPayPeriod, lastPayPeriod);
            var expectedPayPeriod = new FederalPayPeriod(eighthPayPeriodStartDate, eighthPayPeriodStartDate.AddDays(payPeriodLength - 1), 8);
            var actualPPReturned = payCalendar.GetCurrentPayPeriod(eighthPayPeriodSampleDate);

            Assert.Equal(expectedPayPeriod.StartDate, actualPPReturned.StartDate);
            Assert.Equal(expectedPayPeriod.EndDate, actualPPReturned.EndDate);
            Assert.Equal(expectedPayPeriod.Period, actualPPReturned.Period);
        }
    }
}