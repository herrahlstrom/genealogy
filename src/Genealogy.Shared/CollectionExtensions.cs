using System;
using System.Linq;

namespace Genealogy;

public static class CollectionExtensions
{
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        if (collection is List<T> list)
        {
            list.AddRange(collection);
        }
        else
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
