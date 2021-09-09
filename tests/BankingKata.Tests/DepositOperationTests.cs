using BankingKata.Operations;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingKata.Tests
{
    public class DepositOperationTests
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
        public void WhenApplied_ShouldReturnAccountBalanceWithAmount()
        {
            var operation = new DepositOperation(GenerateTestCurrency(100));
            _accountMock.Setup(a => a.GetBalance()).Returns(300);

            var newBalance = operation.Apply(_accountMock.Object);

            newBalance.Should().Be(400);
        }
    }
}