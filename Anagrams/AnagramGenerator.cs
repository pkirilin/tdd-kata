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
            
            // return ExecuteInternal(input)
            //     .Distinct()
            //     .ToArray();

            // return ExecuteInternal(" " + input)
            //     .Select(x => x.Remove(0, 1))
            //     .Distinct()
            //     .ToArray();

            return GetInputVariations(input)
                .SelectMany(ExecuteInternal)
                .Distinct()
                .ToArray();
        }

        private static string[] ExecuteInternal(string input)
        {
            var head = input.First();
            var tail = new string(input.Skip(1).ToArray());
            
            if (tail.Length == 0)
            {
                return new[] {head.ToString()};
            }
            
            if (tail.Length == 1)
            {
                if (head == tail[0])
                {
                    return new[] { $"{head}{tail[0]}" };
                }
                
                return new[]
                {
                    $"{head}{tail[0]}",
                    $"{tail[0]}{head}"
                };
            }

            return GetInputVariations(tail)
                .SelectMany(ExecuteInternal)
                .SelectMany(t => new[] { $"{head}{t}" })
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
                var variation = new string(tail.Concat(new[] { head }).ToArray());
                variations[idx++] = variation;
            
                currentVariation = variation;
                sourceSymbols = currentVariation.ToArray();
            }
            
            return variations;
        }
    }
}