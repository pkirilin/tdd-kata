using System;
using System.Collections.Generic;
using System.Text;

namespace RomanNumerals
{
    public static class RomanNumeralsConverter
    {
        private static readonly char[] RomanDigitsOne = {'I', 'X', 'C', 'M'};
        private static readonly char[] RomanDigitsFive = {'V', 'L', 'D'};
        
        public static string Convert(int number)
        {
            var result = new StringBuilder();
            var numberParts = SplitNumber(number);

            foreach (var numberPart in numberParts)
                result.Append(ConvertNumberPart(numberPart));

            return result.ToString();
        }

        private static string ConvertNumberPart(int number)
        {
            var countZeros = GetCountDigits(number) - 1;
            var firstDigit = number / (int) Math.Pow(10, countZeros);

            if (countZeros > 3)
            {
                var repeatCount = number / (int) Math.Pow(10, 3);
                return new string(RomanDigitsOne[3], repeatCount);
            }

            switch (firstDigit)
            {
                case < 4:
                    return new string(RomanDigitsOne[countZeros], firstDigit);
                case 4:
                    return new string(new[]
                    {
                        RomanDigitsOne[countZeros],
                        RomanDigitsFive[countZeros]
                    });
                case 5:
                    return RomanDigitsFive[countZeros].ToString();
                case 9:
                    return new string(new[]
                    {
                        RomanDigitsOne[countZeros],
                        RomanDigitsOne[countZeros + 1]
                    });
                default:
                {
                    var repeatCount = firstDigit - 5;
                    return RomanDigitsFive[countZeros] + new string(RomanDigitsOne[countZeros], repeatCount);
                }
            }
        }

        private static IEnumerable<int> SplitNumber(int number)
        {
            var splittedNumbers = new Stack<int>();
            var countDigits = GetCountDigits(number);

            for (var i = 0; i < countDigits; i++)
            {
                var numberPart = number % 10 * (int) Math.Pow(10, i);
                if (numberPart > 0)
                    splittedNumbers.Push(numberPart);
                number /= 10;
            }

            return splittedNumbers;
        }

        private static int GetCountDigits(int number)
        {
            var count = 0;
            
            while (number > 0)
            {
                count++;
                number /= 10;
            }

            return count;
        }
    }
}
