using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager.DataLayer
{
    public partial class PayrollJob
    {

        public PayrollJob()
        {
            
            this.PayrollItems.AssociationChanged += PayrollItems_AssociationChanged;
        }


        void PayrollItems_AssociationChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            OnPropertyChanged("TotalIncome");
            OnPropertyChanged("TotalDeductions");
            OnPropertyChanged("NetAmount"); 
        }

       




        public string Name { get; set; }
        public double TotalIncome { get; set; }
        public double TotalDeductions { get; set; }
        //public string Name
        //{
        //    get
        //    {
        //        return String.Format("{2}: {0:MMM-dd-yy} - {1:MMM-dd-yy}", StartDate, EndDate, PayrollJobType.Name);
        //    }
        //}
        public int DurationInDays
        {
            get
            {
                TimeSpan diff = (EndDate - StartDate);

                return diff.Days;
            }
        }
        //public double TotalIncome
        //{
        //    get
        //    {
        //        if (Branch != null)
        //        {
        //            if(PayrollItems != null)
        //                return PayrollItems.Where(p => p.IncomeDeduction == true && p.ParentPayrollItem == null && p.Employee != null && p.Employee.InstitutionId == Branch.InstitutionId).Sum(p => p.Amount);
        //            return 0;
        //        }
        //        else
        //        {
        //            return PayrollItems.Where(p => p.IncomeDeduction == true && p.ParentPayrollItem == null).Sum(p => p.Amount);
        //        }
        //    }
        //}

        //public double TotalDeductions
        //{
        //    get
        //    {
        //        if (Branch != null)
        //        {
        //            return PayrollItems.Where(p => p.IncomeDeduction == false && p.ParentPayrollItem == null && p.Employee != null && p.Employee.InstitutionId == Branch.InstitutionId).Sum(p => p.Amount);
        //        }
        //        else
        //        {
        //            return PayrollItems.Where(p => p.IncomeDeduction == false && p.ParentPayrollItem == null).Sum(p => p.Amount);
        //        }
        //    }
        //}

        public double NetAmount => TotalIncome - TotalDeductions;
    }
}
