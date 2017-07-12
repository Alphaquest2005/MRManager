using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterNut.DataLayer
{
    public partial class xcuda_PreviousItem
    {
        public string DutyFreePaid
        {
            get
            {
                return xcuda_Item.xcuda_ASYCUDA.xcuda_Identification.xcuda_Type.DisplayName == "EX9" ? "Duty Free" : "Duty Paid";
            }            
        }
    }
}
