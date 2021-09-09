using BankingKata.Operations;
using BankingKata.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace BankingKata.Tests
{
    public class DepositOperationTests
    {
        [Fact]
        public void WhenApplied_ShouldReturnAccountBalanceWithAmount()
        {
            var operation = new DepositOperation(Create.Currency(100));
            var account = Create.AccountWithBalance(300);

            var newBalance = operation.Apply(account);

            newBalance.Should().Be(400);
        }
    }
}