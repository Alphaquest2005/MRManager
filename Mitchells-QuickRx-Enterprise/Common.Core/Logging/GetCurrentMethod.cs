using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Logging
{
    public static class GetCurrentMethodClass
    {
        public static string GetCurrentMethod([CallerMemberName] string meth = "", [CallerLineNumber] int line = 0 )
        {
            return meth + ":" + line;
        }
    }
}
