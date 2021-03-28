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
        
        [Theory]
        [InlineData("1", 1)]
        [InlineData("2", 2)]
        public void When_OneNumberSpecified_Should_ReturnThatNumber(string numbers, int expected)
        {
            // Act
            var sum = Calculator.Add(numbers);

            // Assert
            Assert.Equal(expected, sum);
        }
        
        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("3,4", 7)]
        public void When_TwoNumbersSpecified_Should_ReturnSum(string numbers, int expected)
        {
            // Act
            var sum = Calculator.Add(numbers);

            // Assert
            Assert.Equal(expected, sum);
        }
        
        [Theory]
        [InlineData("1,2,3,4,5", 15)]
        public void When_UnknownAmountOfNumbersSpecified_Should_ReturnSum(string numbers, int expected)
        {
            // Act
            var sum = Calculator.Add(numbers);

            // Assert
            Assert.Equal(expected, sum);
        }
    }
}