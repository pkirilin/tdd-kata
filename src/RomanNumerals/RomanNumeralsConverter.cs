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
                if (number == 4)
                {
                    result.Append("IV");
                    number -= 4;
                    continue;
                }
                
                if (number == 5)
                {
                    result.Append('V');
                    number -= 5;
                    continue;
                }

                result.Append('I');
                number--;
            }
            
            return result.ToString();
        }
    }
}
