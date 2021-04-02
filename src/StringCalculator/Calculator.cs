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

            var delimeters = new List<string>() {",", "\n"};
            var numbersData = numbers;
            
            if (numbers.StartsWith("//"))
            {
                var customDelimeter = new string(numbers.Skip(2)
                    .Where(ch => ch != '[' && ch != ']')
                    .TakeWhile(ch => ch != '\n')
                    .ToArray());
                
                delimeters.Add(customDelimeter);
                numbersData = numbers.Remove(0, numbers.IndexOf('\n') + 1);
            }

            var numbersToSum = numbersData.Split(delimeters.ToArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .Where(n => n <= 1000)
                .ToArray();
            
            var negativeNumbers = numbersToSum.Where(n => n < 0)
                .ToArray();

            if (negativeNumbers.Any())
            {
                throw new InvalidOperationException($"negatives not allowed: {string.Join(", ", negativeNumbers)}");
            }

            return numbersToSum.Sum();
        }
    }
}