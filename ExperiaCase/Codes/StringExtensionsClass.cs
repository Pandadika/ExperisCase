using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperiaCase.Codes
{
    public static class StringExtensionsClass
    {
        public static string? Capitalize(this string str)
        {
            str = str.Trim();
            if (str == null) return null;
            else if (str.Length == 0) return str;
            else if (str.Length == 1) return str.ToUpper();
            else return char.ToUpper(str[0])+str.Substring(1).ToLower();
        }
    }
}
