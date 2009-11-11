using System;
using System.Collections.Generic;

namespace XmlExplorer
{
    public static class IListExtensions
    {
        public static void RemoveAll<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T item = list[i];

                if (!predicate(item))
                    continue;

                list.RemoveAt(i);

                i--;
            }
        }
    }
}
