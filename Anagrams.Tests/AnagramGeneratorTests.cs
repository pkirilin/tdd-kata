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
    }
}