using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionMasteryGg
{
    public static class SystemExtensions
    {
        public static int LastDigits(this string input)
        {
            string digits = string.Empty;

            for (int i = input.Length - 1; i < input.Length; i--)
            {
                if (Char.IsDigit(input[i]))
                    digits += input[i];
                else
                    break;
            }

            if (digits.Length > 0)
                return int.Parse(digits);
            else
                throw new ArgumentException("The string didn't end in digits!");
        }

        public static int ToInt(this string input) => int.Parse(input);
        public static double ToDouble(this string input) => double.Parse(input);

        public static string AfterLast(this string input, char value) =>
            input.Substring(input.LastIndexOf(value) + 1);

        public static string Between(this string input, string left, string right)
        {
            int pFrom = input.IndexOf(left) + right.Length;
            int pTo = input.IndexOf(right, pFrom);

            return input.Substring(pFrom, pTo - pFrom);
        }            
    }
}
