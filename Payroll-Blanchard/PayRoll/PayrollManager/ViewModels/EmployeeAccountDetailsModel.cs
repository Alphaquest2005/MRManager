using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;
using PayrollManager.DataLayer;
using System.Data;

namespace PayrollManager
{
	public class EmployeeAccountDetailsModel : BaseViewModel
	{
        public EmployeeAccountDetailsModel()
		{
		    using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
		    {
		        _employeeAccountTypes = new ListCollectionView(ctx.AccountTypes.ToList());
		    }

		}

	    private readonly ListCollectionView _employeeAccountTypes;
        public ListCollectionView EmployeeAccountTypes => _employeeAccountTypes;


	    public void SaveEmployeeAccount()
        {
            if (CurrentEmployeeAccount == null) return;

            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                
                if (CurrentEmployeeAccount.AccountId == 0)
                {
                    ctx.Accounts.AddObject(CurrentEmployeeAccount);
                }
                else
                {
                    if (CurrentEmployeeAccount.EntityState == EntityState.Added) return;
                    var ritm = ctx.Accounts.First(x => x.AccountId == CurrentEmployeeAccount.AccountId);
                    ctx.Accounts.Attach(ritm);
                    ctx.Accounts.ApplyCurrentValues(CurrentEmployeeAccount);
                }

                SaveDatabase(ctx);
            }



            OnStaticPropertyChanged("CurrentEmployeeAccount");
            OnStaticPropertyChanged("EmployeeAccounts");

        }

    

      
        public void EditAccount(DataLayer.Account acc)
        {
            CurrentAccount = acc;
        }

     

        public void DeleteEmployeeAccount()
        {
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                if (CurrentEmployeeAccount == null) return;
                ctx.Accounts.Attach(CurrentEmployeeAccount);
                ctx.Accounts.DeleteObject(CurrentEmployeeAccount);
                // db.PayrollItems.Detach(CurrentPayrollItem);


                SaveDatabase(ctx);
                CurrentEmployeeAccount = null;
            }
        }


        public void NewEmployeeAccount()
        {
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                DataLayer.EmployeeAccount newemp = ctx.Accounts.CreateObject<DataLayer.EmployeeAccount>();
                ctx.Accounts.AddObject(newemp);
                CurrentEmployeeAccount = newemp;
                
            }
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
	}
}