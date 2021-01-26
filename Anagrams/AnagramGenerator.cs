using System;
using System.Linq;

namespace Anagrams
{
    public static class AnagramGenerator
    {
        public static string[] Execute(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Array.Empty<string>();
            }

            if (input.Length == 1)
            {
                return new[] {input};
            }

            if (input.Length == 2)
            {
                if (input[0] == input[1])
                {
                    return new[] {$"{input[0]}{input[1]}"};
                }

                return new[]
                {
                    $"{input[0]}{input[1]}",
                    $"{input[1]}{input[0]}"
                };
            }

            return GetInputVariations(input)
                .SelectMany(variation =>
                    GetInputVariations(new string(variation.Skip(1).ToArray()))
                        .SelectMany(Execute)
                        .SelectMany(tail => new[] {$"{variation.First()}{tail}"}))
                .Distinct()
                .ToArray();
        }

        private static string[] GetInputVariations(string source)
        {
            var variations = new string[source.Length];
            variations[0] = source;
            var idx = 1;

            var currentVariation = source;
            var sourceSymbols = currentVariation.ToArray();

            while (idx < source.Length)
            {
                var head = sourceSymbols[0];
                var tail = currentVariation.Skip(1);
                var variation = new string(tail.Concat(new[] {head}).ToArray());
                variations[idx++] = variation;

                currentVariation = variation;
                sourceSymbols = currentVariation.ToArray();
            }

            return variations;
        }
    }
}