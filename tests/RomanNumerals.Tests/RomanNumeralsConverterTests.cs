using FluentAssertions;
using Xunit;

namespace RomanNumerals.Tests
{
    public class RomanNumeralsConverterTests
    {
        [Fact]
        public void ShouldReturn_I_WhenInputIs_1()
        {
            RomanNumeralsConverter.Convert(1).Should().Be("I");
        }
        
        [Fact]
        public void ShouldReturn_II_WhenInputIs_2()
        {
            RomanNumeralsConverter.Convert(2).Should().Be("II");
        }
    }
}
