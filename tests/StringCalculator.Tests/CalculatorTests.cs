using System;
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
        
        [Fact]
        public void When_NumbersAreSeparatedWithLinesOrCommas_Should_ReturnSum()
        {
            // Act
            var sum = Calculator.Add("1\n2,3");

            // Assert
            Assert.Equal(6, sum);
        }
        
        [Fact]
        public void When_NumbersAreSeparatedWithCustomDelimeters_Should_ReturnSum()
        {
            // Act
            var sum = Calculator.Add("//;\n1;2");

            // Assert
            Assert.Equal(3, sum);
        }

        [Theory]
        [InlineData("-1", "negatives not allowed: -1")]
        [InlineData("1,-2,3,-4", "negatives not allowed: -2, -4")]
        public void When_NegativeNumbersAreSpecified_Should_ThrowException(string numbers, string expectedMessage)
        {
            var exception = Assert.Throws<InvalidOperationException>(() =>
            {
                Calculator.Add(numbers);
            });
            
            Assert.Equal(expectedMessage, exception.Message);
        }
        
        [Fact]
        public void When_NumbersContainOnesBiggerThan1000_Should_ReturnSumWithoutThatNumbers()
        {
            // Act
            var sum = Calculator.Add("2,1001");

            // Assert
            Assert.Equal(2, sum);
        }
        
        [Fact]
        public void When_NumbersContainDelimeterOfAnyLength_Should_ReturnSum()
        {
            // Act
            var sum = Calculator.Add("//[***]\n1***2***3");

            // Assert
            Assert.Equal(6, sum);
        }
    }
}