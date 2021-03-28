using System.Linq;

namespace StringCalculator
{
    public static class Calculator
    {
        public static int Add(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
                return 0;

            return numbers.Split(',', '\n')
                .Select(int.Parse)
                .Sum();
        }
    }
}