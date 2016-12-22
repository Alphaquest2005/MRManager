using System;
using System.Collections.Generic;

namespace Utilities
{
    public static class TypeNameExtensions
    {
        public static string GetFriendlyName(this Type type)
        {
            string friendlyName = type.Name;
            if (type.IsGenericType)
            {
                int iBacktick = friendlyName.IndexOf('`');
                if (iBacktick > 0)
                {
                    friendlyName = friendlyName.Remove(iBacktick);
                }
                friendlyName += "<";
                Type[] typeParameters = type.GetGenericArguments();
                for (int i = 0; i < typeParameters.Length; ++i)
                {
                    string typeParamName = "";
                    if (typeParameters[i].IsGenericType)
                    {
                        typeParamName = typeParameters[i].GetFriendlyName();
                    }
                    else
                    {
                        typeParamName = typeParameters[i].Name;
                    }
                    
                    friendlyName += (i == 0 ? typeParamName : "," + typeParamName);
                }
                friendlyName += ">";
            }

            return friendlyName;
        }

        public static T Type2Generic<T>(this T ttype)
        {
           
            if (typeof(T).IsGenericType)
            {
                var type = typeof(T).GetGenericTypeDefinition();
                Type[] typeParameters = type.GetGenericArguments();
                List<Type> paramTypes = new List<Type>();
                for (int i = 0; i < typeParameters.Length; ++i)
                {
                    
                    if (typeParameters[i].IsGenericType)
                    {
                        paramTypes.Add(typeParameters[i].Type2Generic());
                    }
                    else
                    {
                        paramTypes.Add(typeParameters[i]);
                    }

                    
                }
                return (T)(object)type.GetType().MakeGenericType(paramTypes.ToArray());
            }

            
            
            return (T)(object)ttype;
        }
    }
}
