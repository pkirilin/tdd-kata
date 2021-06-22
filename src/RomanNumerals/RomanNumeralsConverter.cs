using System.Collections.Generic;
using System.Text;

namespace RomanNumerals
{
    public static class RomanNumeralsConverter
    {
        // 8 -> VIII
        // 8 - 5 = 3 (5 -> "V", "V")
        // 3 - 1 = 2 (1 -> "I", "VI")
        // 2 - 1 = 1 (1 -> "I", "VII")
        // 1 - 1 = 0 (1 -> "I", "VIII")
        
        public static string Convert(int number)
        {
            var result = new StringBuilder();
            
            var arabicToRomanParts = new Dictionary<int, string>()
            {
                [1] = "I",
                [4] = "IV",
                [5] = "V",
            };

            while (number > 0)
            {
                var maxNumberToSubtract = 0;
                
                foreach (var pair in arabicToRomanParts)
                {
                    var arabicPart = pair.Key;
                    
                    if (arabicPart > maxNumberToSubtract && number - arabicPart >= 0)
                    {
                        maxNumberToSubtract = arabicPart;
                    }
                }

                if (!arabicToRomanParts.ContainsKey(maxNumberToSubtract))
                    break;
                
                result.Append(arabicToRomanParts[maxNumberToSubtract]);
                number -= maxNumberToSubtract;
            }
            
            return result.ToString();
        }
    }
}
