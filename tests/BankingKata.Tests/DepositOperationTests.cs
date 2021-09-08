using BankingKata.Operations;
using FluentAssertions;
using Moq;
using Xunit;

namespace BankingKata.Tests
{
    public class DepositOperationTests
    {
        private readonly Mock<IAccount> _accountMock = new();
        
        [Fact]
        public void WhenApplied_ShouldReturnAccountBalanceWithAmount()
        {
            var operation = new DepositOperation(100);
            _accountMock.Setup(a => a.GetBalance()).Returns(300);

            var newBalance = operation.Apply(_accountMock.Object);

            newBalance.Should().Be(400);
        }
    }
}