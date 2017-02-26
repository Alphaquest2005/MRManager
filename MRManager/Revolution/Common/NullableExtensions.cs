using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class NullableExtensions
    {
        public static Nullable<T> ToNullable<T>(this string s) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(s);
                }
            }
            catch { }
            return result;
        }

        public static dynamic ToNullable(this object s,Type type) 
        {
            try
            {
               TypeConverter conv = TypeDescriptor.GetConverter(type);
               var result = conv.ConvertFrom(s);
               return result;
            }
            catch { }
            return null;
        }
    }
}
