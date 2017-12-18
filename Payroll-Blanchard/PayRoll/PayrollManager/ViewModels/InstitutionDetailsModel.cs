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
	public class InstitutionDetailsModel : BaseViewModel
	{
       

        public void SaveInstitution()
        {
            if (CurrentInstitution == null) return;
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                if (CurrentInstitution.InstitutionId == 0)
                {
                    ctx.Institutions.AddObject(CurrentInstitution);
                }
                else
                {
                    if (CurrentInstitution.EntityState == EntityState.Added) return;
                    var ritm = ctx.Institutions.First(x => x.InstitutionId == CurrentInstitution.InstitutionId);
                     ctx.Institutions.Attach(ritm);
                ctx.Institutions.ApplyCurrentValues(CurrentInstitution);
                }
               
                SaveDatabase(ctx);
            }
            
           
            OnStaticPropertyChanged("CurrentInstitutionAccount");
           // CycleInstitutionAccounts();
            OnStaticPropertyChanged("InstitutionAccounts");

        }






        public void EditAccount(DataLayer.Institution acc)
        {
            CurrentInstitution = acc;
        }

        public void DeleteInstition()
        {
            if (CurrentInstitution == null) return;
            if (CurrentInstitution.InstitutionId != 0)
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                var ritm = ctx.Institutions.First(x => x.InstitutionId == CurrentInstitution.InstitutionId);
                ctx.Institutions.DeleteObject(ritm);
                SaveDatabase(ctx);
            }
            CurrentInstitution = null;

        }

        


       

        public void NewInstitution()
        {
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                DataLayer.Institution newemp = ctx.Institutions.CreateObject();
                ctx.Institutions.AddObject(newemp);

                CurrentInstitution = newemp;
            }
            OnPropertyChanged("CurrentInstitution");
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