using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlExplorer
{
    public class EventArgs<T> : EventArgs
    {
        public T Item { get; private set; }

        public EventArgs(T item)
            : base()
        {
            this.Item = item;
        }
    }
}
