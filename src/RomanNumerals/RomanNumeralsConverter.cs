using System;
using System.Collections.Generic;
using System.Text;

namespace RomanNumerals
{
    public static class RomanNumeralsConverter
    {
        private static readonly IReadOnlyDictionary<int, char> RomanDigitsOne = new Dictionary<int, char>()
        {
            [0] = 'I',
            [1] = 'X',
            [2] = 'C',
            [3] = 'M',
        };
        
        private static readonly IReadOnlyDictionary<int, char> RomanDigitsFive = new Dictionary<int, char>()
        {
            [0] = 'V',
            [1] = 'L',
            [2] = 'D',
        };
        
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
                splittedNumbers.Push(number % 10 * (int) Math.Pow(10, i));
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
