using System;
using System.Linq;

namespace Anagrams
{
    public static class AnagramGenerator
    {
        public static string[] Execute(string input)
        {
            // TODO: add test
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
    }
}