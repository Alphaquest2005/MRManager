using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Data.Linq_Extensions
{
   public static class IEnumerable
    {
        public static IEnumerable<TSource> NullToEmpty<TSource>(
    this IEnumerable<TSource> source)
        {
            if (source == null)
                return Enumerable.Empty<TSource>();

            return source;
        }
    }
}
