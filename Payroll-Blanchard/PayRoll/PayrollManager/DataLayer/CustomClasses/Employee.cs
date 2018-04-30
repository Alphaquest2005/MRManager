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
       

    }
}
