using System;
using BankingKata.Operations;
using BankingKata.Tests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingKata.Tests
{
    public class WithdrawOperationTests
    {
        private readonly Mock<IAccount> _accountMock = new();

        [Fact]
        public void WhenAmountIsLessOrEqualAccountBalance_ShouldReturnAccountBalanceWithoutAmount()
        {
            var operation = new WithdrawOperation(Create.Currency(100));
            _accountMock.Setup(a => a.GetBalance()).Returns(300);

            var newBalance = operation.Apply(_accountMock.Object);

            newBalance.Should().Be(200);
        }
        
        [Fact]
        public void WhenAmountIsGreaterThanAccountBalance_ShouldThrowException()
        {
            var operation = new WithdrawOperation(Create.Currency(100));
            _accountMock.Setup(a => a.GetBalance()).Returns(50);
            
            Action action = () =>
            {
                operation.Apply(_accountMock.Object);
            };

            action.Should().Throw<InvalidOperationException>();
        }
    }
}