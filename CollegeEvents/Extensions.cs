using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollegeEvents
{
    public static class Extensions
    {
        public static bool IsSQLValid(this string s)
        {
            if (s.All((c) =>
                char.IsLetterOrDigit(c) &&
                char.IsPunctuation(c) && c != '"' && c != '\''
            ))
                return false;
            
            return true;
        }
    }
}
