using System;
using BankingKata.Operations;
using BankingKata.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace BankingKata.Tests
{
    public class WithdrawOperationTests
    {
        [Fact]
        public void WhenAmountIsLessOrEqualAccountBalance_ShouldReturnAccountBalanceWithoutAmount()
        {
            var operation = new WithdrawOperation(Create.Currency(100));
            var account = Create.AccountWithBalance(300);

            var newBalance = operation.Apply(account);

            newBalance.Should().Be(200);
        }
        
        [Fact]
        public void WhenAmountIsGreaterThanAccountBalance_ShouldThrowException()
        {
            var operation = new WithdrawOperation(Create.Currency(100));
            var account = Create.AccountWithBalance(300);
            
            Action action = () =>
            {
                operation.Apply(account);
            };

            action.Should().Throw<InvalidOperationException>();
        }
    }
}