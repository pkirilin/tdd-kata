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
                
                return new[]
                {
                    $"{input[0]}{input[1]}{input[2]}", $"{input[0]}{input[2]}{input[1]}",
                    $"{input[1]}{input[0]}{input[2]}", $"{input[1]}{input[2]}{input[0]}",
                    $"{input[2]}{input[0]}{input[1]}", $"{input[2]}{input[1]}{input[0]}", 
                };
            }

            return Array.Empty<string>();
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