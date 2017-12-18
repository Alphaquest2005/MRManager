using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using PayrollManager.DataLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace PayrollManager
{
	public class IntroScreenModel :BaseViewModel
	{
		public IntroScreenModel()
		{
			staticPropertyChanged +=IntroScreenModel_staticPropertyChanged;
            
		}

        private void IntroScreenModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName == "PayrollItems")
            {
                OnStaticPropertyChanged("PayrollJobs");
                OnPropertyChanged("NetAmount");
            }

            if (e.PropertyName == "CurrentCompany" || e.PropertyName == "CurrentYear")
            {
                OnStaticPropertyChanged("PayrollJobs");
            }
        }

	    public int[] YearLst
	    {
	        get
	        {
	            if (CurrentCompany == null || CurrentCompany.PayrollJobs.Any() == false) return new int[] {DateTime.Now.Year};
	            return CurrentCompany.PayrollJobs.Select(x => x.StartDate.Year).Distinct().ToArray();
	        }
	    }

       

	}
}