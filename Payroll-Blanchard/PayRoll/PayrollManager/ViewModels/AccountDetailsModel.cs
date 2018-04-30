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
		
        public void SaveAccount()
        {


            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                if (CurrentAccount == null) return;
                if (CurrentAccount.AccountId == 0)
                {
                    ctx.Accounts.AddObject(CurrentAccount);
                }
                else
                {
                    if (CurrentAccount.EntityState == EntityState.Added) return;
                    var ritm = ctx.Accounts.First(x => x.AccountId == CurrentAccount.AccountId);
                ctx.Accounts.Attach(ritm);
                ctx.Accounts.ApplyCurrentValues(CurrentAccount);
                }
                
                SaveDatabase(ctx);
            }
            LoadInstitutions();
            LoadInstitutionsAndCompanies();
            CurrentAccount = null;
            OnStaticPropertyChanged("CurrentAccount");
           // CycleAccounts();
            OnStaticPropertyChanged("Accounts");

        }






        public void EditAccount(DataLayer.InstitutionAccounts acc)
        {
            CurrentAccount = acc;
        }

	    public void DeleteInstitionAccount()
	    {
            if (CurrentAccount == null) return;
            if (CurrentAccount.AccountId != 0)
	            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	            {
	                var ritm = ctx.Accounts.First(x => x.AccountId == CurrentAccount.AccountId);
	                ctx.Accounts.DeleteObject(ritm);
	                SaveDatabase(ctx);

	            }
	        CurrentAccount = null;
            LoadInstitutions();
            LoadInstitutionsAndCompanies();
	        OnStaticPropertyChanged("CurrentAccount");
	        // CycleAccounts();
	        OnStaticPropertyChanged("Accounts");
        }






	    public void NewAccount()
        {
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                DataLayer.Account newemp =
                    ctx.Accounts.CreateObject<DataLayer.InstitutionAccounts>();
                ctx.Accounts.AddObject(newemp);

                CurrentAccount = newemp;
                OnPropertyChanged("CurrentAccount");
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