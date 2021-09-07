using System;
using BankingKata.Abstractions;
using FluentAssertions;
using Xunit;

namespace BankingKata.Tests
{
    public class AccountTests
    {
        private static IAccount TestAccount => new Account();
        
        [Fact]
        public void WhenSeriesOfActionsPerformed_ShouldPrintActionsHistoryStatement()
        {
            var account = TestAccount;
            account.Deposit(500);
            account.Withdraw(100);

            var statement = account.PrintStatement();

            statement.Should().Be(
                $"Date: {DateTime.Now:yyyy-MM-dd}, amount: +500, balance: 500\n" +
                $"Date: {DateTime.Now:yyyy-MM-dd}, amount: -100, balance: 400\n");
        }
    }
}
