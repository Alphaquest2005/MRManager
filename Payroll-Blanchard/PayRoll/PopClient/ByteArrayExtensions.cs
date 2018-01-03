using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopClient
{
    public static class ByteArrayExtensions
    {
        public static string GetString(this byte[] source, int count)
        {
            return Encoding.UTF8.GetString(source, 0, count);
        }

        public static string GetString(this byte[] source)
        {
            return source.GetString(source.Length);
        }
    }
}
