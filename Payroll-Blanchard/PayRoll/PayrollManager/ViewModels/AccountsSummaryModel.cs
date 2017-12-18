using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;
using System.Data.Entity;

namespace PayrollManager
{
	public class AccountsSummaryModel : BaseViewModel
	{
        public AccountsSummaryModel()
        {
            staticPropertyChanged += AccountsSummaryModel_staticPropertyChanged;
        }

        void AccountsSummaryModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName == "InstitutionAccountsData")
            {
                OnPropertyChanged("InstitutionAccountsTotalCredit");
                OnPropertyChanged("InstitutionAccountsTotalDebit");
                OnPropertyChanged("InstitutionAccountsTotal");
            }
        }

        public double InstitutionAccountsTotalCredit
        {
            get
            {
                return HybridAccountsLst.Sum(x => x.TotalCredit);
            }
        }

        public double InstitutionAccountsTotalDebit
        {
            get
            {
                return HybridAccountsLst.Sum(x => x.TotalDebit);
            }
        }

        public double InstitutionAccountsTotal
        {
            get
            {
                return InstitutionAccountsTotalCredit - InstitutionAccountsTotalDebit;
            }
        }


	}
}