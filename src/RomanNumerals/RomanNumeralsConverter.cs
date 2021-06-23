using System;
using System.Collections.Generic;
using System.Text;

namespace RomanNumerals
{
    public static class RomanNumeralsConverter
    {
        // 1999 = 1000 + 900 + 90 + 9 = M + CM + XC + IX
        
        // 1234 = 1000 + 200 + 30 + 4 = M + CC + XXX + IV
        
        // 1...3 -> I...III
        // 4 -> IV
        // 5 -> V
        // 6...8 -> VI...VIII
        // 9 -> IX
        
        // 10 -> X
        // 20 -> XX
        
        // 5 -> V
        // 50 -> L
        // 500 -> D
        
        // 7 -> VII
        // 70 -> LXX
        // 700 -> DCC
        
        // 4 -> IV
        // 40 -> XL
        // 400 -> CD
        
        // 9 -> IX
        // 90 -> XC
        // 900 -> CM

        public static string Convert(int number)
        {
            var result = new StringBuilder();

            var numberParts = SplitNumber(number);

            foreach (var numberPart in numberParts)
            {
                result.Append(ConvertNumberPart(numberPart));
            }

            return result.ToString();
        }

        private static string ConvertNumberPart(int number)
        {
            var countZeros = GetCountDigits(number) - 1;
            var firstDigit = number / (int) Math.Pow(10, countZeros);

            var romanDigitsOne = new Dictionary<int, char>()
            {
                [0] = 'I',
                [1] = 'X',
                [2] = 'C'
            };
            
            var romanDigitsFive = new Dictionary<int, char>()
            {
                [0] = 'V',
                [1] = 'L',
                [2] = 'D'
            };
            
            switch (firstDigit)
            {
                case < 4:
                    return new string(romanDigitsOne[countZeros], firstDigit);
                case 4:
                    return new string(new[]
                    {
                        romanDigitsOne[countZeros],
                        romanDigitsFive[countZeros]
                    });
                case 5:
                    return romanDigitsFive[countZeros].ToString();
                case 9:
                    return new string(new[]
                    {
                        romanDigitsOne[countZeros],
                        romanDigitsOne[countZeros + 1]
                    });
                default:
                {
                    var repeatCount = firstDigit - 5;
                    return romanDigitsFive[countZeros] + new string(romanDigitsOne[countZeros], repeatCount);
                }
            }
        }

        private static IEnumerable<int> SplitNumber(int number)
        {
            var splittedNumbers = new Stack<int>();
            var countDigits = GetCountDigits(number);

            for (var i = 0; i < countDigits; i++)
            {
                splittedNumbers.Push(number % 10 * (int)Math.Pow(10, i));
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
