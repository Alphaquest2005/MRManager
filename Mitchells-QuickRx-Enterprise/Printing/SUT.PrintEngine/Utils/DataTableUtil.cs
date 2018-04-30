using System;
using System.Data;
using System.Linq;

namespace SUT.PrintEngine.Utils
{
    internal class DataTableUtil
    {
        public static void Validate(DataTable dataTable)
        {
            if (dataTable == null) return;
            var result = true;
            foreach (DataColumn column in dataTable.Columns)
            {
                if(column != null && column.ExtendedProperties.ContainsKey("Width"))
                {
                    throw new FormatException(string.Format("Column Width not Defined for column : '{0}'", column.ColumnName));
                }
            }
        }
    }
}
