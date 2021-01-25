using System;
using System.Linq;

namespace Anagrams
{
    public static class AnagramGenerator
    {
        public static string[] Execute(string input)
        {
            // TODO: add test
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            
            if (input.Length == 0)
            {
                return Array.Empty<string>();
            }
            
            if (input.Length == 1)
            {
                return new[] { input.ElementAt(0).ToString() };
            }

            if (input.Length == 2)
            {
                return GetOutputTails(new[] { input[0], input[1] });
            }

            return input
                .SelectMany(x => ComplexCase(x, input.Where(y => x != y).ToArray()))
                .ToArray();
        }
        
        private static string[] ComplexCase(char head, char[] tail)
        {
            if (tail.Length == 3)
            {
                var tempTails = tail.SelectMany(head => ComplexCase(head, tail.Where(y => y != head).ToArray()));
                return tempTails
                    .Select(y => $"{head}{y}")
                    .ToArray();
            }
            
            return GetOutputTails(tail)
                .Select(y => $"{head}{y}")
                .ToArray();
        }

        private static string[] GetOutputTails(char[] tail)
        {
            return tail[0] == tail[1]
                ? new[] {$"{tail[0]}{tail[1]}"}
                : new[] {$"{tail[0]}{tail[1]}", $"{tail[1]}{tail[0]}"};
        }
    }
}