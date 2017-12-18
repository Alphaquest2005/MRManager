using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager.DataLayer
{
   public partial class PayrollSetupItem
    {
       public string DisplayValue
       {
           get
           {
               
               return Amount == null ? string.Format("{0:p}", Rate) : string.Format("{0:c}", Amount);
           }
       }
    }
}
