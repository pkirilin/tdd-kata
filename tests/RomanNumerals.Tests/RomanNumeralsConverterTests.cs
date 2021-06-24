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
        
        [Fact]
        public void ShouldReturn_III_WhenInputIs_3()
        {
            RomanNumeralsConverter.Convert(3).Should().Be("III");
        }
        
        [Fact]
        public void ShouldReturn_IV_WhenInputIs_4()
        {
            RomanNumeralsConverter.Convert(4).Should().Be("IV");
        }
        
        [Fact]
        public void ShouldReturn_V_WhenInputIs_5()
        {
            RomanNumeralsConverter.Convert(5).Should().Be("V");
        }
        
        [Fact]
        public void ShouldReturn_VIII_WhenInputIs_8()
        {
            RomanNumeralsConverter.Convert(8).Should().Be("VIII");
        }
        
        [Fact]
        public void ShouldReturn_XXI_WhenInputIs_21()
        {
            RomanNumeralsConverter.Convert(21).Should().Be("XXI");
        }
        
        [Fact]
        public void ShouldReturn_XLIX_WhenInputIs_49()
        {
            RomanNumeralsConverter.Convert(49).Should().Be("XLIX");
        }
        
        [Fact]
        public void ShouldReturn_MCCXXXIV_WhenInputIs_1234()
        {
            RomanNumeralsConverter.Convert(1234).Should().Be("MCCXXXIV");
        }
        
        [Fact]
        public void ShouldReturn_MCMXCIX_WhenInputIs_1999()
        {
            RomanNumeralsConverter.Convert(1999).Should().Be("MCMXCIX");
        }
        
        [Fact]
        public void ShouldReturn_MMMMMMMMMM_WhenInputIs_10000()
        {
            RomanNumeralsConverter.Convert(10000).Should().Be("MMMMMMMMMM");
        }
        
        [Fact]
        public void ShouldReturn_MMMMMMMMMMMMCCCXLV_WhenInputIs_12345()
        {
            RomanNumeralsConverter.Convert(12345).Should().Be("MMMMMMMMMMMMCCCXLV");
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ShouldReturnEmptyString_WhenInputIsLessThanOrEqualZero(int number)
        {
            RomanNumeralsConverter.Convert(number).Should().BeEmpty();
        }
    }
}
