using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;

namespace PayrollManager
{
	public class AccountsSummaryReportModel : BaseViewModel
	{
		public AccountsSummaryReportModel()
		{
            staticPropertyChanged += AccountsSummaryReportModel_staticPropertyChanged;
		}

        void AccountsSummaryReportModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName == "InstitutionAccounts")
            {
                OnPropertyChanged("AccountsSummaryData");
            }
        }

        public List<DataLayer.AccountEntry> AccountsSummaryData
        {
            get
            {
                var alst = from a in InstitutionAccounts.SelectMany( x => x.CurrentAccountEntries.ToList())
                           select a;
                return alst.ToList();
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