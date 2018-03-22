using System;
using Xunit;
using AutomatedTeller;
using static AutomatedTeller.Program;

namespace AutomatedTellerTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(450.0, 50.0, 400.0)]
        [InlineData(675.15, 23.74, 651.41)]
        [InlineData(1337.42, 692.83, 644.59)]
        [InlineData(0.30, 0.15, 0.15)]
        public void WithdrawFundsTest(decimal currentBalance,
            decimal withdrawalAmount, decimal expectedResult)
        {
            Assert.Equal(expectedResult,
                WithdrawFunds(currentBalance, withdrawalAmount));
        }

        [Theory]
        [InlineData(450.0, 50.0, 500.0)]
        [InlineData(675.15, 23.74, 698.89)]
        [InlineData(1337.42, 692.83, 2030.25)]
        [InlineData(0.30, 0.15, 0.45)]
        public void DepositFundsTest(decimal currentBalance,
            decimal depositAmount, decimal expectedResult)
        {
            Assert.Equal(expectedResult,
                DepositFunds(currentBalance, depositAmount));
        }

        [Theory]
        [InlineData(386.50, "$386.50")]
        [InlineData(50.3, "$50.30")]
        [InlineData(0.37, "$0.37")]
        [InlineData(0.0, "$0.00")]
        public void ViewBalanceTest(decimal currentBalance,
            string expectedResult)
        {
            Assert.Equal(expectedResult, ViewBalance(currentBalance));
        }
    }
}
