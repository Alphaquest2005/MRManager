using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager.DataLayer
{
    public partial class Branch
    {
        public Branch()
        {
            
        }

       

        public int TotalEmployees
        {
            get
            {
                return Employees.Count;
            }
        }


    }
}
