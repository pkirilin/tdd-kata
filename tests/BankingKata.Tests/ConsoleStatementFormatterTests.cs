using System;
using System.Collections.Generic;
using BankingKata.History;
using BankingKata.Operations;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingKata.Tests
{
    public class ConsoleStatementFormatterTests
    {
        private readonly Mock<IAccount> _accountMock = new();

        private static IEnumerable<StatementItem> GenerateStatementItemsForTest()
        {
            yield return new StatementItem(new DepositOperation(GenerateTestCurrency(500)), 500);
            yield return new StatementItem(new WithdrawOperation(GenerateTestCurrency(100)), 400);
        }

        // TODO: move to fixtures
        private static ICurrency GenerateTestCurrency(int amount)
        {
            var currencyMock = new Mock<ICurrency>();
            currencyMock.Setup(c => c.GetAmount()).Returns(amount);
            return currencyMock.Object;
        }

        [Fact]
        public void ShouldFormatStatementForAccount()
        {
            var formatter = new ConsoleStatementFormatter();
            _accountMock.Setup(a => a.GetStatement()).Returns(GenerateStatementItemsForTest());

            var statementString = formatter.Format(_accountMock.Object);

            statementString.Should().Be($"Date: {DateTime.Now:yyyy-MM-dd}, amount: +500, balance: 500\n" +
                                        $"Date: {DateTime.Now:yyyy-MM-dd}, amount: -100, balance: 400\n");
        }
    }
}