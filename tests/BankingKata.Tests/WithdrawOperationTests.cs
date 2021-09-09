using System;
using BankingKata.Operations;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingKata.Tests
{
    public class WithdrawOperationTests
    {
        private readonly Mock<IAccount> _accountMock = new();

        // TODO: move to fixtures
        private static ICurrency GenerateTestCurrency(int amount)
        {
            var currencyMock = new Mock<ICurrency>();
            currencyMock.Setup(c => c.GetAmount()).Returns(amount);
            return currencyMock.Object;
        }
        
        [Fact]
        public void WhenAmountIsLessOrEqualAccountBalance_ShouldReturnAccountBalanceWithoutAmount()
        {
            var operation = new WithdrawOperation(GenerateTestCurrency(100));
            _accountMock.Setup(a => a.GetBalance()).Returns(300);

            var newBalance = operation.Apply(_accountMock.Object);

            newBalance.Should().Be(200);
        }
        
        [Fact]
        public void WhenAmountIsGreaterThanAccountBalance_ShouldThrowException()
        {
            var operation = new WithdrawOperation(GenerateTestCurrency(100));
            _accountMock.Setup(a => a.GetBalance()).Returns(50);
            
            Action action = () =>
            {
                operation.Apply(_accountMock.Object);
            };

            action.Should().Throw<InvalidOperationException>();
        }
    }
}