using System;
using System.Globalization;

namespace NumberService
{
    public static class Number
    {
        public const string _EQUAL = "equal to";
        public const string _GREATER = "greater than";
        public const string _LESS = "less than";
        public const string _ERROR = "One or both input strings not convertible to Double";

        public static string CompareNumbers(string num1, string num2)
        {
            //double type of string inputs
            double dNum1;
            double dNum2;

            //whether the strings are convertable to doubles
            bool num1IsNumber = false;
            bool num2IsNumber = false;
            
            //return string
            string comparison = "";

            num1IsNumber = Double.TryParse(num1, NumberStyles.Any, CultureInfo.InvariantCulture, out dNum1);
            num2IsNumber = Double.TryParse(num2, NumberStyles.Any, CultureInfo.InvariantCulture, out dNum2);
        
            if(num1IsNumber && num2IsNumber)
            {
                string equality =  dNum1 == dNum2 ? _EQUAL : dNum1 > dNum2 ? _GREATER : _LESS;
                comparison = string.Format("{0} is {1} {2}", num1, equality, num2);    
            }
            else
            {
                comparison = _ERROR; 
            }
            return comparison;
        }
    }
}