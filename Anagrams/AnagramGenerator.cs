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
                return new[] { input };
            }
            
            return Array.Empty<string>();
        }
    }
}