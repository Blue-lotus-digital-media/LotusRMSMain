using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Utility
{
    public static class NumberToWord
    {
        public static string MoneyToWords(double value)
        {
            decimal money = Math.Round((decimal)value, 2);
            int number = (int)money;
            int decimalValue = 0;
            string doller = string.Empty;
            string cents = string.Empty;
            doller = NumberToWords(number);
            if (money.ToString().Contains("."))
            {
                decimalValue = int.Parse(money.ToString().Split('.')[1]);
                cents = NumberToWords(decimalValue);
            }
            string result = !string.IsNullOrEmpty(cents) ? (decimalValue == 1 ? string.Format("{0} Rupees and {1} Cent Only.", doller, cents) : string.Format("{0} Doller and {1} Paisa Only.", doller, cents)) : string.Format("{0} Rupees Only.", doller);
            return result;
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[(number) / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[(number) % 10];
                }
            }
            return words;
        }
    }
}
