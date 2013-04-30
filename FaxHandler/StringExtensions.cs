using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Extensions
{
    // trim all spaces, even those inside the string
    public static string TrimAll(this string s)
    {
        string result = new string((from c in s.ToCharArray()
                                    where !Char.IsWhiteSpace(c)
                                    select c).ToArray());
        return result;
    }
    public static bool IsValidFilenameChar(this char c)
    {
        if (!c.IsPrint() || c > 0xff)
            return false;
        return !("<>:\"/\\|?*+=%".Contains(c));
    }
    public static bool IsPrint(this char c)
    {
        if (c < 0x20 || c >= 0x7f)
            return false;
        return true;
    }
}
