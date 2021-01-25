using System;
using System.Linq;

namespace Anagrams
{
    public static class AnagramGenerator
    {
        public static string[] Execute(string input)
        {
            if (input.Length == 1)
            {
                return new[] { input.ElementAt(0).ToString() };
            }

            if (input.Length == 2)
            {
                return SimpleCase(input[0], input[1]);
            }

            if (input.Length > 2)
            {
                // idx -> 0, 1, 2
                // input[curIdx]

                var one = ComplexCase(input[0], input.Where(x => x != input[0]).ToArray());
                var two = ComplexCase(input[1], input.Where(x => x != input[1]).ToArray());
                var three = ComplexCase(input[2], input.Where(x => x != input[2]).ToArray());
                return one.Concat(two).Concat(three).ToArray();
            }

            return Array.Empty<string>();
        }

        private static string[] ComplexCase(char x1, char[] tail)
        {
            return SimpleCase(tail[0], tail[1])
                .Select(y => $"{x1}{y}")
                .ToArray();
        }

        private static string[] SimpleCase(char x1, char x2)
        {
            if (x1 == x2)
            {
                return new[] { $"{x1}{x2}" };
            }
            
            return new[] { $"{x1}{x2}", $"{x2}{x1}" };
        }
    }
}