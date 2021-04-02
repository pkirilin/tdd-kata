using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public static class Calculator
    {
        public static int Add(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
                return 0;

            var delimiters = new List<string>() {",", "\n"};
            var numbersString = string.Copy(inputString);
            
            if (inputString.HasCustomDelimiters())
            {
                var customDelimiters = inputString.GetCustomDelimiters();
                delimiters.AddRange(customDelimiters);
                numbersString = inputString.RemoveFirstLine();
            }

            var numbers = numbersString.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(n => n <= 1000)
                .ToArray();
            var negativeNumbers = numbers.Where(n => n < 0).ToArray();

            if (negativeNumbers.Any())
                throw new InvalidOperationException($"negatives not allowed: {string.Join(", ", negativeNumbers)}");

            return numbers.Sum();
        }

        private static bool HasCustomDelimiters(this string str)
        {
            return str.StartsWith("//");
        }

        private static IEnumerable<string> GetCustomDelimiters(this string str)
        {
            var customDelimitersLine = new string(str.Skip(2)
                .TakeWhile(ch => ch != '\n')
                .ToArray());
            
            return customDelimitersLine.Split('[', ']');
        }

        private static string RemoveFirstLine(this string str)
        {
            return str.Remove(0, str.IndexOf('\n') + 1);
        }
    }
}