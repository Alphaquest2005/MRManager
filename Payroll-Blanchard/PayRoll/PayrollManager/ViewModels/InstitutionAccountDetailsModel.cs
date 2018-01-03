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
	public class InstitutionAccountDetailsModel : BaseViewModel
	{
       

        public void SaveInstitutionAccount()
        {
            if (CurrentInstitutionAccount == null) return;
            var instId = CurrentInstitutionAccount.InstitutionId;
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                if (CurrentInstitutionAccount.AccountId == 0)
                {
                    ctx.Accounts.AddObject(CurrentInstitutionAccount);
                }
                else
                {
                    if (CurrentInstitutionAccount.EntityState == EntityState.Added) return;
                    var ritm = ctx.Accounts.OfType<InstitutionAccount>().First(x => x.AccountId == CurrentInstitutionAccount.AccountId);
                     ctx.Accounts.Attach(ritm);
                ctx.Accounts.ApplyCurrentValues(CurrentInstitutionAccount);
                }
               
                SaveDatabase(ctx);
            }

            LoadInstitutions();
            CycleCurrentInstitution(instId);
            OnStaticPropertyChanged("CurrentInstitutionAccount");
           // CycleInstitutionAccounts();
            OnStaticPropertyChanged("InstitutionAccounts");
            
        }


        public void DeleteInstitutionAccount()
        {
            if (CurrentInstitutionAccount == null) return;
            var instId = CurrentInstitutionAccount.InstitutionId;
            if (CurrentInstitutionAccount.AccountId != 0)

            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                var ritm = ctx.Accounts.OfType<InstitutionAccount>().First(x => x.AccountId == CurrentInstitutionAccount.AccountId);
                    ctx.Accounts.DeleteObject(ritm);
                SaveDatabase(ctx);
            }
            CurrentInstitutionAccount = null;
            LoadInstitutions();
            CycleCurrentInstitution(instId);
        }

        


       

        public void NewInstitutionAccount()
        {
            CurrentInstitutionAccount = new InstitutionAccount();
            if (CurrentInstitution != null) CurrentInstitutionAccount.InstitutionId = CurrentInstitution.InstitutionId;
            OnPropertyChanged("CurrentInstitutionAccount");
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