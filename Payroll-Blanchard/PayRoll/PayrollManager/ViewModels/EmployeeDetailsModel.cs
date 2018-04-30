using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class EmployeeDetailsModel : BaseViewModel
	{
        public void SaveEmployee()
        {
            if (CurrentEmployee == null) return;
            if (string.IsNullOrEmpty(CurrentEmployee.LastName) || string.IsNullOrEmpty(CurrentEmployee.FirstName) ||
                CurrentEmployee.CompanyId == null)
            {
                MessageBox.Show("Employee Must Have First Name, Last Name and Location.");
                return;
            }
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                
                if (CurrentEmployee.EmployeeId == 0)
                {
                    ctx.Employees.AddObject(CurrentEmployee);
                }
                else
                {
                    if (CurrentEmployee.EntityState == EntityState.Added) return;
                    var ritm = ctx.Employees.First(x => x.EmployeeId == CurrentEmployee.EmployeeId);
                    ctx.Employees.Attach(ritm);
                    ctx.Employees.ApplyCurrentValues(CurrentEmployee);
                }
                
                SaveDatabase(ctx);
            }
            LoadEmployees();
            OnStaticPropertyChanged("CurrentEmployee");
            OnStaticPropertyChanged("Employees");


        }


        public void EmployeeAutoSetup()
        {
            SaveEmployee();
            if (CurrentEmployee != null) AutoSetupEmployee(CurrentEmployee);
           
            MessageBox.Show("Setup Complete");
        }

        public void DeleteEmployee()
        {
            if (CurrentEmployee == null)
            {
                MessageBox.Show("Please select an employee");
                return;
            }
            if (CurrentEmployee.PayrollItems.Any())
            {
                MessageBox.Show("Cannot Delete an Employee with Payroll Items, Please set 'Employment End Date' for employee.");
                return;
            }
            if (CurrentEmployee.EmployeeAccounts.Any())
            {
                MessageBox.Show("Please Delete Employee Accounts before deleting employee.");
                return;
            }
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                
                if (CurrentEmployee.EmployeeId != 0)
                {
                    if (CurrentEmployee.EntityState == EntityState.Added) return;
                    var ritm = ctx.Employees.FirstOrDefault(x => x.EmployeeId == CurrentEmployee.EmployeeId);
                    
                    ctx.Employees.DeleteObject(ritm);
                    SaveDatabase(ctx);
                }
                CurrentEmployee = null;
                LoadEmployees();
                OnStaticPropertyChanged("CurrentEmployee");
                OnStaticPropertyChanged("Employees");
            }
        }

	    public void NewEmployee()
	    {
	        using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	        {
	            var newemp = ctx.Employees.CreateObject<DataLayer.Employee>();
	            ctx.Employees.AddObject(newemp);
	            CurrentEmployee = newemp;
	        }
	        OnPropertyChanged("CurrentEmployee");

	    }




	    #region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		#endregion

        internal void DeleteEmployeeAccount(DataLayer.EmployeeAccount p)
        {
            if(p.AccountId != 0)
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                var ritm = ctx.EmployeeAccounts.FirstOrDefault(x => x.AccountId == p.AccountId);
                ctx.EmployeeAccounts.DeleteObject(ritm);
                OnStaticPropertyChanged("CurrentEmployee");
                OnStaticPropertyChanged("EmployeeAccounts");
            }
            p = null;
        }

        internal void EditAccount(DataLayer.Account a)
        {
            CurrentAccount = a;
        }

      
    }
}