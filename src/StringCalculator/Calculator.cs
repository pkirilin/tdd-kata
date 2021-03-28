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

            var delimeters = new List<char>() {',', '\n'};
            var numbersData = numbers;
            
            if (numbers.StartsWith("//"))
            {
                delimeters.AddRange(numbers.Skip(2).TakeWhile(ch => ch != '\n'));
                numbersData = numbers.Remove(0, numbers.IndexOf('\n') + 1);
            }

            var allNumbers = numbersData.Split(delimeters.ToArray())
                .Select(int.Parse)
                .ToArray();
            
            var negativeNumbers = allNumbers.Where(n => n < 0)
                .ToArray();

            if (negativeNumbers.Any())
            {
                throw new InvalidOperationException($"negatives not allowed: {string.Join(", ", negativeNumbers)}");
            }

            return allNumbers.Sum();
        }
    }
}