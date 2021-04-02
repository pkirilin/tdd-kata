using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public static class Calculator
    {
        public static int Add(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
                return 0;

            var delimiters = new List<string>() {",", "\n"};
            var numbersData = numbers;
            
            if (numbers.HasCustomDelimiters())
            {
                var customDelimiters = numbers.GetCustomDelimiters();
                delimiters.AddRange(customDelimiters);
                numbersData = numbers.RemoveFirstLine();
            }

            var numbersToSum = numbersData.Split(delimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(n => n <= 1000)
                .ToArray();
            var negativeNumbers = numbersToSum.Where(n => n < 0).ToArray();

            if (negativeNumbers.Any())
            {
                throw new InvalidOperationException($"negatives not allowed: {string.Join(", ", negativeNumbers)}");
            }

            return numbersToSum.Sum();
        }

        private static bool HasCustomDelimiters(this string numbers)
        {
            return numbers.StartsWith("//");
        }

        private static IEnumerable<string> GetCustomDelimiters(this string numbers)
        {
            var customDelimitersLine = new string(numbers.Skip(2)
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