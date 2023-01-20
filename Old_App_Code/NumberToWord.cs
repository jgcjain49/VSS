using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin_CommTrex
{
    public class NumberToWord
    {
        private static string format_rupees_and_paise = "{0} AND {1} PAISE ONLY";

        private static string[] unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX",
                                                   "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE",
                                                   "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };

        private static string[] tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY",
                                                  "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

        internal static string ConvertNumberToWords(decimal number)
        {
            int intPortion = (int)number;
            int decPortion = (int)((number - intPortion) * (decimal)100); string intPortionString = ConvertNumbertoWords(intPortion);
            return string.Format(format_rupees_and_paise, (countOccurences(" AND", intPortionString) > 0 ? intPortionString.Replace(" AND", "") : intPortionString), ConvertNumbertoWords(decPortion));
        }

        protected static string ConvertNumbertoWords(long number)
        {
            if (number == 0)
                return "ZERO";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));

            string words = "";

            if ((number / 100000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKHS ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "AND ";
                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }

            return words;
        }

        private static string RemoveDoubleSpace(string inputString)
        {
            return inputString.Replace("  ", " ");
        }

        private static int countOccurences(string needle, string haystack)
        {
            return (haystack.Length - haystack.Replace(needle, "").Length) / needle.Length;
        }
    }
}