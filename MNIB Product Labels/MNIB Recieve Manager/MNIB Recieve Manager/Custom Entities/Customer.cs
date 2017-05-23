using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MNIB_Distribution_Manager
{
   public partial class Customer
    {
        public string Info
        {
            get { return CustomerName + " - " + CustomerAddress; }
        }
    }
}
