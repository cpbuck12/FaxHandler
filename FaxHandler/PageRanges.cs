using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace FaxHandler
{
    public class PageRanges : IEnumerable<PageRange>
    {
        static public PageRanges All = new PageRanges();
        PageRange[] pageRanges;

        private PageRanges()
        {
        }
        public int LastPage
        {
            get
            {
                return (from range in pageRanges
                        select range.End).Max();
            }
        }
        public PageRanges(string value)
        {
            string s = value.TrimAll();
            if (s == string.Empty)
                throw new FormatException("Page Ranges empty");


            string[] commastrings = s.Split(',');
            List<PageRange> ranges = new List<PageRange>();
            foreach (string commastring in commastrings)
            {
                string[] minusstrings = commastring.Split('-');
                PageRange pageRange = new PageRange();
                if (minusstrings.Length == 1)
                {
                    pageRange.Begin = pageRange.End = int.Parse(minusstrings[0]);
                }
                else
                {
                    if (minusstrings[0] == string.Empty)
                        throw new FormatException("Page Range with begining");
                    if (minusstrings[1] == string.Empty)
                        throw new FormatException("Page Range without ending");
                    pageRange.Begin = int.Parse(minusstrings[0]);
                    pageRange.End = int.Parse(minusstrings[1]);
                    if (pageRange.Begin >= pageRange.End)
                        throw new FormatException("Page Range out of order");
                }
                ranges.Add(pageRange);
            }
            PageRange lastRange = null;
            foreach (var range in (from r in ranges orderby r.Begin select r))
            {
                if (lastRange == null)
                    lastRange = range;
                else
                {
                    if (lastRange.Overlap(range))
                        throw new ArgumentException("Overlapping pages");
                    lastRange = range;
                }
                pageRanges = ranges.ToArray();
            }
        }

        IEnumerator<PageRange> IEnumerable<PageRange>.GetEnumerator()
        {
            return pageRanges.Cast<PageRange>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
