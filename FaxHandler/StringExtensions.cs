using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class StringExtensions
{
    // trim all spaces, even those inside the string
    public static string TrimAll(this string s)
    {
        string result = new string((from c in s.ToCharArray()
                                    where !Char.IsWhiteSpace(c)
                                    select c).ToArray());
        return result;
    }
}
