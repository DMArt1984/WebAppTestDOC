using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAppTestDOC
{
    static public class Validator // Проверка данных
    {
        const string regPhone = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$";

        static public bool Phone(string value)
        {
            Regex regex = new Regex(regPhone);
            return regex.IsMatch(value);
        }

        static public bool IIN(string value)
        {
            // можно использовать регулярные выражения 
            return !String.IsNullOrWhiteSpace(value);
        }

        static public bool FullName(string value)
        {
            // можно использовать регулярные выражения 
            return !String.IsNullOrWhiteSpace(value);
        }
    }
}
