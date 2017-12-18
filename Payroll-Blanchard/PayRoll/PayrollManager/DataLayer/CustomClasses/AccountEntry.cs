using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager.DataLayer
{
    public partial class AccountEntry
    {
        public double Total
        {
            get
            {

                return CreditAmount - DebitAmount;
            }
        }

        //public bool IncomeDeduction
        //{
        //    get
        //    {
        //        return PayrollItem.IncomeDeduction;
        //    }
        //}
    }
}
