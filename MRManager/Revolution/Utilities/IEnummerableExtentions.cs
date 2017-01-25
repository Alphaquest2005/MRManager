using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class IEnummerableExtentions
    {
        public static T Next<T>(this IEnumerable<T> source, T item)
        {
            if (item == null) return default(T);
            var res = source.SkipWhile(x => x.GetHashCode() != item.GetHashCode()).Skip(1).FirstOrDefault();
            if (res == null) return item;
            return res;
        }

        public static T Previous<T>(this IEnumerable<T> source, T item)
        {
            if (item == null) return default(T);
            var lst = source.Reverse();
            var res = lst.SkipWhile(x => x.GetHashCode() != item.GetHashCode()).Skip(1).FirstOrDefault();
            if (res == null) return item;
            return res;
        }
    }
}
