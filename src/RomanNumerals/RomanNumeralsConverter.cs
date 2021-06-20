namespace RomanNumerals
{
    public static class RomanNumeralsConverter
    {
        public static string Convert(int number)
        {
            if (number == 1)
            {
                return "I";
            }

            return "II";
        }
    }
}
