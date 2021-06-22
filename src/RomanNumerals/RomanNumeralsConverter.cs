using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RomanNumerals
{
    public static class RomanNumeralsConverter
    {
        public static string Convert(int number)
        {
            var result = new StringBuilder();
            
            var arabicToRomanNumberParts = new Dictionary<int, string>()
            {
                [1] = "I",
                [4] = "IV",
                [5] = "V",
            };

            while (number > 0)
            {
                var maxArabicNumberToSubtract = arabicToRomanNumberParts.Aggregate(
                    0,
                    (max, numberPart) => numberPart.Key > max && number - numberPart.Key >= 0
                        ? numberPart.Key
                        : max);

                if (!arabicToRomanNumberParts.ContainsKey(maxArabicNumberToSubtract))
                    break;
                
                result.Append(arabicToRomanNumberParts[maxArabicNumberToSubtract]);
                number -= maxArabicNumberToSubtract;
            }
            
            return result.ToString();
        }
    }
}
