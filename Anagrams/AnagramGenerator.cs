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
                if (input.ElementAt(0) == input.ElementAt(1))
                {
                    return new[] { input };
                }
                
                return new[] { input, string.Join("", input.Reverse()) };
            }

            return Array.Empty<string>();
        }
    }
}