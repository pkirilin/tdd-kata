using Xunit;

namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void When_EmptyStringSpecified_Should_ReturnZero()
        {
            // Act
            var sum = Calculator.Add("");

            // Assert
            Assert.Equal(0, sum);
        }
        
        [Fact]
        public void When_OneNumberSpecified_Should_ReturnThatNumber()
        {
            // Act
            var sum = Calculator.Add("1");

            // Assert
            Assert.Equal(1, sum);
        }
    }
}