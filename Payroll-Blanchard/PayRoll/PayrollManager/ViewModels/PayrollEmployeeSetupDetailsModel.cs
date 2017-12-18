using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows.Controls;
using PayrollManager.DataLayer;
using EntityState = System.Data.EntityState;

namespace PayrollManager
{
	public class PayrollEmployeeSetupDetailsModel : BaseViewModel
	{
      

		public PayrollEmployeeSetupDetailsModel()
		{
            staticPropertyChanged += PayrollEmployeeSetupDetailsModel_staticPropertyChanged;
		}

   
        void PayrollEmployeeSetupDetailsModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            

            OnPropertyChanged(e.PropertyName);

            if (e.PropertyName == "CurrentEmployee" || e.PropertyName == "CurrentPayrollJobType")
            {
                GetPayrollEmployeeSetups();
                

            }
        }

   


	    public void UpdateProperties()
        {
            
           
            OnStaticPropertyChanged("PayrollEmployeeSetup");
            OnStaticPropertyChanged("CurrentEmployee");
           

        }


        public ListCollectionView ChargeTypes
        {
            get
            {
                using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
                {
                    return new ListCollectionView(ctx.ChargeTypes.ToList());
                }
            }
        }

	  


	    public void SavePayrollEmployeeSetup()
	    {
            if (CurrentPayrollEmployeeSetup == null) return;
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	        {
	            if (CurrentPayrollEmployeeSetup.PayrollEmployeeSetupId == 0)
	            {
	                ctx.PayrollEmployeeSetup.AddObject(CurrentPayrollEmployeeSetup);

	            }
	            else
	            {
	                if (CurrentPayrollEmployeeSetup.EntityState == EntityState.Added) return;
                    var ritm = ctx.PayrollEmployeeSetup.First(
	                    x => x.PayrollEmployeeSetupId == CurrentPayrollEmployeeSetup.PayrollEmployeeSetupId);
	                ctx.PayrollEmployeeSetup.Attach(ritm);
	                ctx.PayrollEmployeeSetup.ApplyCurrentValues(CurrentPayrollEmployeeSetup);
                    
	            }

	            SaveDatabase(ctx);
	            

            }

	        OnStaticPropertyChanged("PayrollEmployeeSetup");

	    }

	    public void DeletePayrollEmployeeSetup(PayrollEmployeeSetup pi)
	    {
            
            if (pi.PayrollEmployeeSetupId != 0)
	        using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	        {
	            var ritm = ctx.PayrollEmployeeSetup.First(x => x.PayrollEmployeeSetupId == pi.PayrollEmployeeSetupId);
                ctx.PayrollEmployeeSetup.DeleteObject(ritm);
                SaveDatabase(ctx);
	        }
	        pi = null;
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