using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class EmployeePayrollItemsListModel:  BaseViewModel
	{
		public EmployeePayrollItemsListModel()
		{
            staticPropertyChanged += EmployeePayrollItemsListModel_PropertyChanged;
           
        
		}

        private void EmployeePayrollItemsListModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((e.PropertyName == "CurrentEmployee" || e.PropertyName == "CurrentCompany" || e.PropertyName == "PayrollItems" || e.PropertyName == "CurrentPayrollJob") && CurrentEmployee != null && CurrentCompany != null && CurrentPayrollJob != null)
            {
                using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
                {
                    var lst = (from p in ctx.PayrollItems//.AsEnumerable()
                        where p.EmployeeId == CurrentEmployee.EmployeeId &&
                              p.PayrollJobId == CurrentPayrollJob.PayrollJobId && p.PayrollJob.Company != null &&
                              p.PayrollJob.Company.InstitutionId == CurrentCompany.InstitutionId
                        select p).ToList();
                    PayrollItemList = lst.Any() 
                        ? new ObservableCollection<DataLayer.PayrollItem>(lst) 
                        : null;
                    OnPropertyChanged("PayrollItemList");
                }
            }
            if (CurrentEmployee == null)
            {
                PayrollItemList = null;
            }

        }

        ObservableCollection<DataLayer.PayrollItem> _payrollItemList;
        public ObservableCollection<DataLayer.PayrollItem> PayrollItemList
        {
            get
            {
                return _payrollItemList;
            }
            set
            {
                _payrollItemList = value;
                OnPropertyChanged("PayrollItemList");
                
            }
        }
        
	}
}