using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace PayrollManager
{
	public class PayCheckViewModel : BaseViewModel
	{
		public PayCheckViewModel()
		{
            staticPropertyChanged += PayCheckViewModel_staticPropertyChanged;
		}

        void PayCheckViewModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentEmployee")
            {
                OnPropertyChanged("CurrentEmployee");
            }
        }

        //public ObservableCollection<DataLayer.AccountEntry> Debits
        //{
        //    get
        //    {
        //        return new ObservableCollection<DataLayer.AccountEntry>(CurrentAccount.AccountEntries.Where(ae => ae.DebitAmount != 0 && ae.PayrollItem.PayrollJobId == BaseViewModel.CurrentPayrollJob.PayrollJobId));
        //    }
        //}

        //public ObservableCollection<DataLayer.AccountEntry> Credits
        //{
        //    get
        //    {
        //        return new ObservableCollection<DataLayer.AccountEntry>(CurrentAccount.AccountEntries.Where(ae => ae.CreditAmount != 0 && ae.PayrollItem.PayrollJobId == BaseViewModel.CurrentPayrollJob.PayrollJobId));
        //    }
        //}
		


	}
}