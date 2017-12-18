using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtendedGrid.Microsoft.Windows.Controls;

namespace ExtendedGrid.Classes
{
    public class GroupByEventArgs:EventArgs
    {
        public DataGridColumn Column { get; set; }
    }
}
