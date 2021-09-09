using System;
using FluentAssertions;
using Xunit;

namespace BankingKata.Tests
{
    public class CurrencyTests
    {
        [Fact]
        public void WhenCreated_ShouldValidateAndInitializeAmount()
        {
            var currency = new Currency(300);

            currency.GetAmount().Should().Be(300);
        }
        
        [Fact]
        public void WhenCreatedWithNegativeAmount_ShouldThrowException()
        {
            Action action = () =>
            {
                var _ = new Currency(-100);
            };

            action.Should().Throw<ArgumentException>();
        }
    }
}