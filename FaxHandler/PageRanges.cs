using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace FaxHandler
{
    internal class Enumerator : IEnumerator<PageRange>
    {
        PageRange IEnumerator<PageRange>.Current
        {
            get { throw new NotImplementedException(); }
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        object IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }

        bool IEnumerator.MoveNext()
        {
            throw new NotImplementedException();
        }

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
    }
    class PageRanges : IEnumerable<PageRange>
    {
        private string text;
        public string Ranges
        {
            set
            {
                Regex r = new Regex(@"(\d+(-\d+)*)+(,\d+(-\d+)*)*");
                text = value.Trim();
                char[] delimeters = { ',' };
                text.Split(delimeters);
            }
        }

        IEnumerator<PageRange> IEnumerable<PageRange>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
