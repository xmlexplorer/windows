using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace XmlExplorer.Controls
{
    public class RecentlyUsedFilesStack : List<String>
    {
        public RecentlyUsedFilesStack() : base()
        {
            this.MaximumItems = 10;
        }

        public int MaximumItems { get; set; }

        public new void Add(string item)
        {
            // remove any existing items with the same value
            while(base.Contains(item))
                base.Remove(item);

            // insert the item at the top of the list
            base.Insert(0, item);

            // remove items from the bottom of the list, if needed
            while (base.Count > MaximumItems)
                base.RemoveAt(base.Count - 1);
        }
    }
}
