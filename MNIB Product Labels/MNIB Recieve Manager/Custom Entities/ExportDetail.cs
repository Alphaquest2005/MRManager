using System;
using BarCodes;


namespace MNIB_Distribution_Manager
{
    public partial class ExportDetail
    { 
        public static string GetBarCode(int exportNo, int lineNumber)
        {
            var tz = new TransPreZeroConverter();
                string val = null;
                val = Convert.ToString(tz.Convert(exportNo.ToString()+ "0"+ lineNumber.ToString(), typeof(string), null, null));
                return val;
            
        }

        public static string GetBarCode(string txn)
        {
            var tz = new TransPreZeroConverter();
            string val = null;
            val = Convert.ToString(tz.Convert(txn, typeof(string), null, null));
            return val;

        }

    }
}
