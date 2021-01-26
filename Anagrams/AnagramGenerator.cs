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

            return GetInputVariations(input)
                .SelectMany(ExecuteInternal)
                .Distinct()
                .ToArray();
        }

        private static string[] ExecuteInternal(string substring)
        {
            if (substring == null)
            {
                return Array.Empty<string>();
            }
            
            var head = substring.First();
            var tail = new string(substring.Skip(1).ToArray());
            
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

                var one = ExecuteInternal(head.ToString());
                var two = ExecuteInternal(tail[0].ToString());
                    
                return new[]
                {
                    $"{one[0]}{two[0]}",
                    $"{two[0]}{one[0]}"
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

        #region Old version

        private static string[] ExecuteV1(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Array.Empty<string>();
            }
            
            return input
                .SelectMany(x => ComplexCase(x, input.Where(y => x != y).ToArray()))
                // .SelectMany(x => ComplexCase(x, input.ToArray()))
                // .Distinct()
                .ToArray();
        }
        
        private static string[] ComplexCase(char head, char[] tail)
        {
            if (tail.Length == 0)
            {
                return new[] { head.ToString() };
            }
            
            if (tail.Length == 1)
            {
                return GetOutputTails(new[] { head, tail[0] });
            }

            var tempTails = tail
                .SelectMany(h => ComplexCase(h, tail.Where(y => y != h).ToArray()))
                .ToArray();
            return tempTails
                .Select(y => $"{head}{y}")
                .ToArray();
        }

        private static string[] GetOutputTails(char[] tail)
        {
            if (tail[0] == tail[1])
            {
                return new[] { tail[0].ToString() };
            }
            
            return new[] {$"{tail[0]}{tail[1]}"};
        }
        
        #endregion
    }
}