using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Extensions
{
    public static class StringExtension
    {
        public static bool ISEmpty(this string s)
        {
            return string.IsNullOrEmpty(s.Trim());
        }
    }
}
