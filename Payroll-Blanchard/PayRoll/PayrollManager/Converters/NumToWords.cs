using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PayrollManager.Converters
{
    public class NumToWords : IValueConverter
    {
//        public static string NumberToWords(int number)
//{
//    if (number == 0)
//        return "zero";

//    if (number < 0)
//        return "minus " + NumberToWords(Math.Abs(number));

//    string words = "";

//    if ((number / 1000000) > 0)
//    {
//        words += NumberToWords(number / 1000000) + " million ";
//        number %= 1000000;
//    }

//    if ((number / 1000) > 0)
//    {
//        words += NumberToWords(number / 1000) + " thousand ";
//        number %= 1000;
//    }

//    if ((number / 100) > 0)
//    {
//        words += NumberToWords(number / 100) + " hundred ";
//        number %= 100;
//    }

//    if (number > 0)
//    {
//        if (words != "")
//            words += "and ";

//        var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
//        var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

//        if (number < 20)
//            words += unitsMap[number];
//        else
//        {
//            words += tensMap[number / 10];
//            if ((number % 10) > 0)
//                words += "-" + unitsMap[number % 10];
//        }
//    }

//    return words;
//}

        public static string ConvertMoneyToText(string value)
        {
            value = value.Replace(",", "").Replace("$", "");
            int decimalCount = 0;
            int Val = value.Length - 1;
            for (int x = 0; x <= Val; x++)
            {
                char Val2 = value[x];
                if (Val2.ToString() == ".")
                {
                    decimalCount++;
                    if (decimalCount > 1)
                    {
                        throw new ArgumentException("Only monetary values are accepted");
                    }
                }
                Val2 = value[x];
                char Valtemp = value[x];
                if (!(char.IsDigit(value[x]) | (Val2.ToString() == ".")) & !((x == 0) & (Valtemp.ToString() == "-")))
                {
                    throw new ArgumentException("Only monetary values are accepted");
                }
            }
            string returnValue = "";
            string[] parts;
            if (value.Contains("."))
                parts = value.Split(new char[] { '.' });
            else
                parts = (value + ".00").Split(new char[] { '.' });


            parts[1] = new string((parts[1] + "00").Substring(0, 2).ToCharArray());
            bool IsNegative = parts[0].Contains("-");
            if (parts[0].Replace("-", "").Length > 0x12)
            {
                throw new ArgumentException("Maximum value is $999,999,999,999,999,999.99");
            }
            if (IsNegative)
            {
                parts[0] = parts[0].Replace("-", "");
                returnValue = returnValue + "Minus ";
            }
            if (parts[0].Length > 15)
            {
                returnValue = ((((returnValue + HundredsText(parts[0].PadLeft(0x12, '0').Substring(0, 3)) + "Quadrillion ")
                    + HundredsText(parts[0].PadLeft(0x12, '0').Substring(3, 3)) + "Trillion ") +
                    HundredsText(parts[0].PadLeft(0x12, '0').Substring(6, 3)) + "Billion ") +
                    HundredsText(parts[0].PadLeft(0x12, '0').Substring(9, 3)) + "Million ") +
                    HundredsText(parts[0].PadLeft(0x12, '0').Substring(12, 3)) + "Thousand ";
            }
            else if (parts[0].Length > 12)
            {
                returnValue = (((returnValue + HundredsText(parts[0].PadLeft(15, '0').Substring(0, 3)) +
                    "Trillion ") + HundredsText(parts[0].PadLeft(15, '0').Substring(3, 3)) + "Billion ") +
                    HundredsText(parts[0].PadLeft(15, '0').Substring(6, 3)) + "Million ") +
                    HundredsText(parts[0].PadLeft(15, '0').Substring(9, 3)) + "Thousand ";
            }
            else if (parts[0].Length > 9)
            {
                returnValue = ((returnValue + HundredsText(parts[0].PadLeft(12, '0').Substring(0, 3)) +
                    "Billion ") + HundredsText(parts[0].PadLeft(12, '0').Substring(3, 3)) + "Million ") +
                    HundredsText(parts[0].PadLeft(12, '0').Substring(6, 3)) + "Thousand ";
            }
            else if (parts[0].Length > 6)
            {
                returnValue = (returnValue + HundredsText(parts[0].PadLeft(9, '0').Substring(0, 3)) +
                    "Million ") + HundredsText(parts[0].PadLeft(9, '0').Substring(3, 3)) + "Thousand ";
            }
            else if (parts[0].Length > 3)
            {
                returnValue = returnValue + HundredsText(parts[0].PadLeft(6, '0').Substring(0, 3)) +
                    "Thousand ";
            }
            string hundreds = parts[0].PadLeft(3, '0');
            int tempInt = 0;
            hundreds = hundreds.Substring(hundreds.Length - 3, 3);
            if (int.TryParse(hundreds, out tempInt) == true)
            {
                if (int.Parse(hundreds) < 100)
                {
                    //returnValue = returnValue + "and ";
                }
                returnValue = returnValue + HundredsText(hundreds) + "Dollar";
                if (int.Parse(hundreds) != 1)
                {
                    returnValue = returnValue + "s";
                }
                if (int.Parse(parts[1]) != 0)
                {
                    returnValue = returnValue + " and ";
                }
            }
            if ((parts.Length == 2) && (int.Parse(parts[1]) != 0))
            {
                returnValue = returnValue + HundredsText(parts[1].PadLeft(3, '0')) + "Cent";
                if (int.Parse(parts[1]) != 1)
                {
                    returnValue = returnValue + "s";
                }
            }
            return returnValue;
        }


        static string[] Tens = new string[] { 
			"Ten",
			"Twenty", 
			"Thirty", 
			"Forty", 
			"Fifty", 
			"Sixty", 
			"Seventy", 
			"Eighty", 
			"Ninety" };
        static string[] Ones = new string[] { 
			"One",
			"Two",
			"Three",
			"Four",
			"Five",
			"Six",
			"Seven",
			"Eight",
			"Nine",
			"Ten",
			"Eleven",
			"Twelve", 
			"Thirteen", 
			"Fourteen", 
			"Fifteen", 
			"Sixteen", 
			"Seventeen", 
			"Eighteen", 
			"Nineteen" };



        private static string HundredsText(string value)
        {
            char Val_1;
            char Val_2;

            string returnValue = "";
            bool IsSingleDigit = true;
            char Val = value[0];
            if (int.Parse(Val.ToString()) != 0)
            {
                Val_1 = value[0];
                returnValue = returnValue + Ones[int.Parse(Val_1.ToString()) - 1] + " Hundred ";
                IsSingleDigit = false;
            }
            Val_1 = value[1];
            if (int.Parse(Val_1.ToString()) > 1)
            {
                Val = value[1];
                returnValue = returnValue + Tens[int.Parse(Val.ToString()) - 1] + " ";
                Val_1 = value[2];
                if (int.Parse(Val_1.ToString()) != 0)
                {
                    Val = value[2];
                    returnValue = returnValue + Ones[int.Parse(Val.ToString()) - 1] + " ";
                }
                return returnValue;
            }
            Val_1 = value[1];
            if (int.Parse(Val_1.ToString()) == 1)
            {
                Val = value[1];
                Val_2 = value[2];
                return (returnValue + Ones[int.Parse(Val.ToString() + Val_2.ToString()) - 1] + " ");
            }
            Val_2 = value[2];
            if (int.Parse(Val_2.ToString()) == 0)
            {
                return returnValue;
            }
            if (!IsSingleDigit)
            {
                returnValue = returnValue + "and ";
            }
            Val_2 = value[2];
            return (returnValue + Ones[int.Parse(Val_2.ToString()) - 1] + " ");
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //int intval;
            //if (int.TryParse(value.ToString(), out intval))
            //{
            //    return NumberToWords(intval) + " Dollars";
            //}
            //else
            //{
            //    return "Not valid number";
            //}
            return ConvertMoneyToText(Math.Round(System.Convert.ToDecimal(value),2).ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
