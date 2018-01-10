using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class CompaniesSummaryListModel : BaseViewModel
	{
		public CompaniesSummaryListModel()
		{
			staticPropertyChanged +=CompaniesSummaryListModel_staticPropertyChanged;

            
		}



	    private void CompaniesSummaryListModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
		{



		    if (e.PropertyName == "Companies")
		    {
		        Companies =  new ObservableCollection<Company>(LoadCompanies());
            }

		    if (e.PropertyName == "CompanyFilter")
		    {
		        Companies = new ObservableCollection<Company>(LoadCompanies().Where(c => c.Institution.Name.ToUpper().Contains(CompanyFilter.ToUpper())));
		    }

		}

		string _companyFilter ;
		public string CompanyFilter
		{
			get => _companyFilter;
		    set
			{
				_companyFilter = value;
				OnStaticPropertyChanged("CompanyFilter");
			}
		}

		
	}
}