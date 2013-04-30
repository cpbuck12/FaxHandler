using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FaxHandler
{
    public class ItemHolder
    {
        public Hashtable values;
        Func<Hashtable,string> toString;
        public ItemHolder(Hashtable values,Func<Hashtable,string> toString)
        {
            this.values = values;
            this.toString = toString;
        }
        override public string ToString()
        {
            return toString(values);
        }
    }
}
