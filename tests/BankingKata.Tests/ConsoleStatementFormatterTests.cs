using System;
using BankingKata.History;
using BankingKata.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace BankingKata.Tests
{
    public class ConsoleStatementFormatterTests
    {
        [Fact]
        public void ShouldFormatStatementForAccount()
        {
            var formatter = new ConsoleStatementFormatter();
            var account = Create.AccountWithStatement(
                Create.DepositStatementItem(500, 500),
                Create.WithdrawStatementItem(100, 400));

            var statementString = formatter.Format(account);

            statementString.Should().Be($"Date: {DateTime.Now:yyyy-MM-dd}, amount: +500, balance: 500\n" +
                                        $"Date: {DateTime.Now:yyyy-MM-dd}, amount: -100, balance: 400\n");
        }
    }
}