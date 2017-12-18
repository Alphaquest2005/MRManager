using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager.DataLayer
{
    public partial class PayrollItem
    {
        public double CreditAmount
        {
            get
            {
                if (this.IncomeDeduction == false) return Amount;
                return 0;
            }
        }
        public double DebitAmount
        {
            get
            {
                if (this.IncomeDeduction == true) return Amount;
                return 0;
            }
        }
    }
}
