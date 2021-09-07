using System;
using FluentAssertions;
using Xunit;

namespace BankingKata.Tests
{
    public class AccountTests
    {
        [Fact]
        public void WhenSeriesOfActionsPerformed_ShouldPrintActionsHistoryStatement()
        {
            var account = new Account();
            account.Deposit(500);
            account.Withdraw(100);

            var statement = account.PrintStatement();

            statement.Should().Be(
                $"Date: {DateTime.Now:yyyy-MM-dd}, amount: +500, balance: 500\n" +
                $"Date: {DateTime.Now:yyyy-MM-dd}, amount: -100, balance: 400\n");
        }
    }
}
