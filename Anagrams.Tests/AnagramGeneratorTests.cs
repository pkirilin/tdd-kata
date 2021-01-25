using System;
using FluentAssertions;
using Xunit;

namespace Anagrams.Tests
{
    public class AnagramGeneratorTests
    {
        [Fact]
        public void WhenGenerateWithEmptyString_ThenReturnsEmptyArray()
        {
            // Arrange
            var input = string.Empty;

            // Act
            var result = AnagramGenerator.Execute(input);

            // Assert
            result.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Fact]
        public void WhenGenerateWithStringWhichContainsOneSymbol_ThenReturnsIt()
        {
            var input = "a";

            var result = AnagramGenerator.Execute(input);

            result.Should().BeEquivalentTo("a");
        }

        [Fact]
        public void WhenGenerateWithStringWhichContainsDuplicateSymbols_ThenReturnsIt()
        {
            var input = "aa";

            var result = AnagramGenerator.Execute(input);

            result.Should().BeEquivalentTo("aa");
        }

        [Fact]
        public void WhenGenerateWithStringWhichContainsTwoSymbols_ThenReturnsItAndReversedString()
        {
            var input = "ab";

            var result = AnagramGenerator.Execute(input);

            result.Should().BeEquivalentTo("ab", "ba");
        }

        [Fact]
        public void WhenGenerateWithStringWhichContainsThreeSymbols_ThenReturnsSixPermutations()
        {
            var input = "abc";

            var result = AnagramGenerator.Execute(input);

            result.Should().BeEquivalentTo("abc", "acb", "bac", "bca", "cab", "cba");
        }
    }
}