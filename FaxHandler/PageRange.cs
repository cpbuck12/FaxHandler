using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FaxHandler
{
    public class PageRange
    {
        public int Begin { set; get; }
        public int End { set; get; }
        public int Length
        {
            get
            {
                return End - Begin;
            }
        }
        bool Between(int less, int value, int more)
        {
            if (less <= value && value <= more)
                return true;
            else
                return false;
        }
        public bool Overlap(PageRange other)
        {
            if (Between(Begin, other.Begin, End) || Between(Begin, other.End, End))
                return true;
            else
                return false;
        }
    }
}
