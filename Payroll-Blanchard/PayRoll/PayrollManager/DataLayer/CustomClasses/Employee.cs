using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager.DataLayer
{
    public partial class Employee
    {
        public Employee()
        {
            this.EmploymentStartDate = DateTime.Now;
            BaseViewModel.staticPropertyChanged += BaseViewModel_staticPropertyChanged;

        }

        void BaseViewModel_staticPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PayrollItems")
            {
                //this.SetBaseAmounts();
                OnPropertyChanged("TotalIncome");
                OnPropertyChanged("TotalDeductions");
                OnPropertyChanged("NetAmount");
            }

            if (e.PropertyName == "PayrollEmployeeSetups")
            {
                //this.SetBaseAmounts();
                OnPropertyChanged("PreTotalIncome");
                OnPropertyChanged("PreTotalDeductions");
                OnPropertyChanged("PreNetAmount");
            }
            //if (e.PropertyName == "AddToBaseAmount")
            //{
            //    SetBaseAmounts();

            //    OnPropertyChanged("PreTotalIncome");
            //    OnPropertyChanged("PreTotalDeductions");
            //    OnPropertyChanged("PreNetAmount");

            //    OnPropertyChanged("TotalIncome");
            //    OnPropertyChanged("TotalDeductions");
            //    OnPropertyChanged("NetAmount");
            //}
        }



        public double Salary { get; set; }
        public double TaxableBenefitsTotal { get; set; }
        public string DisplayName => $"{FirstName} {LastName}";

        public double TotalIncome { get; set; }

        public double TotalDeductions { get; set; }

        public double NetAmount => TotalIncome - TotalDeductions;

        public double? PreNetAmount => PreTotalIncome - PreTotalDeductions;
        public double? PreTotalDeductions { get; set; }
        public double? PreTotalIncome { get; set; }
        //public double Salary
        //{
        //    get
        //    {
        //        return this.PayrollEmployeeSetups.Where(p => p.PayrollSetupItem != null && p.PayrollSetupItem.IncomeDeduction == true && p.PayrollSetupItem.Name == "Salary").Sum(p => p.CalcAmount);//&& p.PayrollJobType == BaseViewModel.CurrentPayrollJobType
        //    }
        //}

        //public double TaxableBenefitsTotal
        //{
        //    get
        //    {
        //        return this.PayrollEmployeeSetups.Where(p => p.PayrollSetupItem != null && p.PayrollSetupItem.IncomeDeduction == true && p.PayrollSetupItem.IsTaxableBenefit == true).Sum(p => p.CalcAmount);//&& p.PayrollJobType == BaseViewModel.CurrentPayrollJobType
        //    }
        //}


        //public string DisplayName
        //{
        //    get
        //    {
        //        return String.Format("{0} {1}", FirstName, LastName);
        //    }
        //}
        //public double TotalIncome
        //{
        //    get
        //    {
        //        if (BaseViewModel.CurrentPayrollJob == null) return 0;
        //        return  PayrollItems.Where(p => p.IncomeDeduction == true && p.ParentPayrollItem == null && p.PayrollJobId == BaseViewModel.CurrentPayrollJob.PayrollJobId).Sum(p => p.Amount);
        //    }
        //}

        //public List<PayrollItem> CurrentPayrollItems
        //{
        //    get
        //    {
        //        return PayrollItems.Where(p => p.PayrollJobId == BaseViewModel.CurrentPayrollJob.PayrollJobId).ToList();
        //    }
        //}

        //public double TotalDeductions
        //{
        //    get
        //    {
        //        if (BaseViewModel.CurrentPayrollJob == null) return 0;
        //        return PayrollItems.Where(p => p.IncomeDeduction == false && p.ParentPayrollItem == null && p.PayrollJobId == BaseViewModel.CurrentPayrollJob.PayrollJobId).Sum(p => p.Amount);
        //    }
        //}

        //public double NetAmount
        //{
        //    get
        //    {
        //        return TotalIncome - TotalDeductions;
        //    }
        //}


        //public double? PreTotalIncome
        //{
        //    get
        //    {

        //        if (BaseViewModel.CurrentPayrollJobType == null) return 0;
        //        double amt =  this.PayrollEmployeeSetups.Where(p => p.PayrollSetupItem != null && p.PayrollSetupItem.IncomeDeduction == true && p.PayrollJobType != null && p.PayrollJobType == BaseViewModel.CurrentPayrollJobType).Sum(p => p.CalcAmount);

        //        return amt ;
        //    }
        //}

        //public double? PreTotalDeductions 
        //{
        //    get
        //    {

        //        if (BaseViewModel.CurrentPayrollJobType == null) return 0;
        //        var plist = PayrollEmployeeSetups.Where(p => p.PayrollSetupItem != null && p.PayrollSetupItem.IncomeDeduction == false &&  p.PayrollJobType == BaseViewModel.CurrentPayrollJobType)Sum(p => p.CalcAmount) * -1;
        //        return plist.;
        //    }
        //}

        //public double? PreNetAmount 
        //{
        //    get
        //    {
        //        return PreTotalIncome - PreTotalDeductions;
        //    }
        //}

    }
}
