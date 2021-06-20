using System.Collections.Generic;
using System.Text;

namespace RomanNumerals
{
    public static class RomanNumeralsConverter
    {
        public static string Convert(int number)
        {
            var result = new StringBuilder();
            
            while (number > 0)
            {
                result.Append('I');
                number--;
            }
            
            return result.ToString();
        }
    }
}
