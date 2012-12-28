using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FaxHandler
{
    class PageRange
    {
        private string text;
        private int begin;
        private int end;
        public int Begin
        {
            get
            {
                if (text == null)
                    throw new Exception("PageRange not initialized");
                return begin;
            }
        }
        public int End
        {
            get
            {
                if (text == null)
                    throw new Exception("PageRange not initialized");
                return end;
            }
        }
        public string Range
        {
            set
            {
                text = value.Trim();
                Regex rSimple = new Regex(@"^\d+$");
                if (rSimple.IsMatch(text))
                {
                    int page = int.Parse(text);
                    if(page == 0)
                    {
                        throw new Exception("No zeroth page");
                    }
                    begin = end = page;
                    return;
                }
                Regex rRange = new Regex(@"(?<begin>\d+)\s*-\s*(?<end>\d+)");
                if (rRange.IsMatch(text))
                {
                    Match match = rRange.Match(text);
                    int begin = int.Parse(match.Groups["begin"].Value);
                    int end = int.Parse(match.Groups["end"].Value);
                    if (begin == 0 || end == 0 || end <= begin)
                        throw new Exception("Bad page range");
                    this.begin = begin;
                    this.end = end;
                }
                else
                    throw new Exception("Invalid format");
            }
        }
    }
}
