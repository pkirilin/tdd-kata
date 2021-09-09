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
    }
}