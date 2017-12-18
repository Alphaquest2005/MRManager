using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class PayrollSetupSummaryModel : BaseViewModel
	{
		public PayrollSetupSummaryModel()
		{
            staticPropertyChanged += summaryModel_PropertyChanged;
                       // PropertyChanged += EmployeePayrollItemsListModel_PropertyChanged;
           UpdateDataSource();
           PayrollSetupItemsList.CollectionChanged += PayrollSetupItemsList_CollectionChanged;

		}

        void PayrollSetupItemsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                foreach (PayrollSetupItem itm in e.NewItems)
                {
                    ctx.PayrollSetupItems.Attach(itm);
                    ctx.PayrollSetupItems.ApplyCurrentValues(itm);
                }
                
                SaveDatabase(ctx);
            }
            OnStaticPropertyChanged("PayrollSetupItems");
        //    UpdateDataSource();
        }

        Int32 _PayrollJobTypeId = -1;
        public  Int32 CurrentPayrollJobTypeId
        {
            get
            {
                return _PayrollJobTypeId;
            }
            set
            {
                _PayrollJobTypeId = value;
                OnStaticPropertyChanged("CurrentPayrollJobTypeId");
            }
        }


        public void summaryModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "CurrentPayrollJobTypeId" && e.PropertyName != "PayrollSetupItemsCollection") return;
            
            OnStaticPropertyChanged("PayrollSetupItemsList");
            UpdateDataSource();
            PayrollSetupItemsList.CollectionChanged += PayrollSetupItemsList_CollectionChanged;
        }

       static ObservableCollection<DataLayer.PayrollSetupItem> _payrollSetupItemsList = null;
        public ObservableCollection<DataLayer.PayrollSetupItem> PayrollSetupItemsList
        {
            get
            {
                if (_payrollSetupItemsList != null) return _payrollSetupItemsList;
                using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
                {
                    _payrollSetupItemsList = new ObservableCollection<PayrollSetupItem>(ctx.PayrollSetupItems);
                }
                return _payrollSetupItemsList;
            }
            set
            {
                _payrollSetupItemsList = value;
                OnPropertyChanged("PayrollSetupItemsList");
                
            }
        }

        public object DataSource{get;set;}

        Boolean _SortReorder = true;
        public bool SortReorder
        {
            get
            {
                return _SortReorder;
            }
            set
            {
                _SortReorder = value;
                UpdateDataSource();

            }
        }

        private void UpdateDataSource()
        {
            if (_SortReorder == true)
            {
                CollectionViewSource pcol = new CollectionViewSource();
                pcol.Source = PayrollSetupItemsList;

                pcol.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
                pcol.SortDescriptions.Add(new SortDescription("Amount", ListSortDirection.Descending));
                pcol.SortDescriptions.Add(new SortDescription("IncomeDeducton", ListSortDirection.Descending));

                pcol.GroupDescriptions.Add(new PropertyGroupDescription("IncomeDeduction"));

                DataSource = pcol.View;

            }
            else
            {
                DataSource = PayrollSetupItemsList;
            }
            OnStaticPropertyChanged("DataSource");
            OnStaticPropertyChanged("SortReorder");
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