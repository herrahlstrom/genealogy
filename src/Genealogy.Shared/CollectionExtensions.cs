using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Genealogy;
public static class CollectionExtensions
{
    public static void AddRange<TItem>(this ICollection<TItem> collection, IEnumerable<TItem> items)
    {
        foreach(var item in items)
        {
            collection.Add(item);
        }
    }
}
