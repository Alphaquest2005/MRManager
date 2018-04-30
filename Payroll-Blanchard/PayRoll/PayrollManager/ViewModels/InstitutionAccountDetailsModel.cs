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
    public class InstitutionalAccountDetailsModel : BaseViewModel
    {


        public void SaveAccount()
        {
            if (CurrentAccount == null) return;
            var instId = CurrentAccount.InstitutionId;
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                if (CurrentAccount.AccountId == 0)
                {
                    ctx.Accounts.AddObject(CurrentAccount);
                }
                else
                {
                    if (CurrentAccount.EntityState == EntityState.Added) return;
                    var ritm = ctx.Accounts.OfType<InstitutionAccounts>().First(x => x.AccountId == CurrentAccount.AccountId);
                    ctx.Accounts.Attach(ritm);
                    ctx.Accounts.ApplyCurrentValues(CurrentAccount);
                }

                SaveDatabase(ctx);
            }

            LoadInstitutions();
            CycleCurrentInstitution(instId);
            OnStaticPropertyChanged("CurrentAccount");
            // CycleAccounts();
            OnStaticPropertyChanged("Accounts");

        }


        public void DeleteAccount()
        {
            if (CurrentAccount == null) return;
            var instId = CurrentAccount.InstitutionId;
            if (CurrentAccount.AccountId != 0)

                using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
                {
                    var ritm = ctx.Accounts.OfType<InstitutionAccounts>().First(x => x.AccountId == CurrentAccount.AccountId);
                    ctx.Accounts.DeleteObject(ritm);
                    SaveDatabase(ctx);
                }
            CurrentAccount = null;
            LoadInstitutions();
            CycleCurrentInstitution(instId);
        }






        public void NewAccount()
        {
            CurrentAccount = new InstitutionAccounts();
            if (CurrentInstitution != null) CurrentAccount.InstitutionId = CurrentInstitution.InstitutionId;
            OnPropertyChanged("CurrentAccount");
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