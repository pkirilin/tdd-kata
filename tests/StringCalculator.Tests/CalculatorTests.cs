using Xunit;

namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void When_EmptyStringSpecified_Should_ReturnZero()
        {
            // Arrange
            var numbers = "";

            // Act
            var sum = Calculator.Add(numbers);

            // Assert
            Assert.Equal(0, sum);
        }
    }
}