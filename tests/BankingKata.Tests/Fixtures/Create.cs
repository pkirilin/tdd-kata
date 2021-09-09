using BankingKata.History;
using BankingKata.Operations;
using Moq;

namespace BankingKata.Tests.Fixtures
{
    public static class Create
    {
        public static ICurrency Currency(int amount)
        {
            var currencyMock = new Mock<ICurrency>();
            currencyMock.Setup(c => c.GetAmount()).Returns(amount);
            return currencyMock.Object;
        }

        public static IAccount AccountWithBalance(int amount)
        {
            var accountMock = new Mock<IAccount>();
            accountMock.Setup(a => a.GetBalance()).Returns(amount);
            return accountMock.Object;
        }

        public static IAccount AccountWithStatement(params StatementItem[] statementItems)
        {
            var accountMock = new Mock<IAccount>();
            accountMock.Setup(a => a.GetStatement()).Returns(statementItems);
            return accountMock.Object;
        }
        
        public static StatementItem DepositStatementItem(int amount, int balance)
        {
            return new StatementItem(
                new DepositOperation(Currency(amount)),
                balance);
        }
        
        public static StatementItem WithdrawStatementItem(int amount, int balance)
        {
            return new StatementItem(
                new WithdrawOperation(Currency(amount)),
                balance);
        }
    }
}