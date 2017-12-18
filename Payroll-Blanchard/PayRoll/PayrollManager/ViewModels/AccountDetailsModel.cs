using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Data;
using System.Linq;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class AccountDetailsModel : BaseViewModel
	{
		
        public void SaveInstitutionAccount()
        {


            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                if (CurrentInstitutionAccount == null) return;
                if (CurrentInstitutionAccount.AccountId == 0)
                {
                    ctx.Accounts.AddObject(CurrentInstitutionAccount);
                }
                else
                {
                    if (CurrentInstitutionAccount.EntityState == EntityState.Added) return;
                    var ritm = ctx.Accounts.First(x => x.AccountId == CurrentInstitutionAccount.AccountId);
                ctx.Accounts.Attach(ritm);
                ctx.Accounts.ApplyCurrentValues(CurrentInstitutionAccount);
                }
                
                SaveDatabase(ctx);
            }
           
           
            OnStaticPropertyChanged("CurrentInstitutionAccount");
           // CycleInstitutionAccounts();
            OnStaticPropertyChanged("InstitutionAccounts");

        }






        public void EditAccount(DataLayer.InstitutionAccount acc)
        {
            CurrentInstitutionAccount = acc;
        }

	    public void DeleteInstitionAccount()
	    {
            if (CurrentInstitutionAccount == null) return;
            if (CurrentInstitutionAccount.AccountId != 0)
	            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	            {
	                var ritm = ctx.Accounts.First(x => x.AccountId == CurrentInstitutionAccount.AccountId);
	                ctx.Accounts.DeleteObject(ritm);
	                SaveDatabase(ctx);

	            }
	        CurrentInstitutionAccount = null;

	    }






	    public void NewInstitutionAccount()
        {
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                DataLayer.InstitutionAccount newemp =
                    ctx.Accounts.CreateObject<DataLayer.InstitutionAccount>();
                ctx.Accounts.AddObject(newemp);

                CurrentInstitutionAccount = newemp;
                OnPropertyChanged("CurrentInstitutionAccount");
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