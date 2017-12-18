using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class PayrollItemsReportModel : BaseViewModel
	{
		public PayrollItemsReportModel()
		{
			staticPropertyChanged +=PayrollItemsReportModel_staticPropertyChanged;
            OnStaticPropertyChanged("CurrentPayrollJob");
		}

        private void PayrollItemsReportModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            if (e.PropertyName == "CurrentPayrollJob")
            {
                if (CurrentPayrollJob != null)
                {
                    GetPayrollItems();

                    OnStaticPropertyChanged("PayrollItems");
                }
                else
                {
                    PayrollItems = null;
                    OnStaticPropertyChanged("PayrollItems");
                }
                OnPropertyChanged("PayrollJobTotal");
            }
        }

	    private void GetPayrollItems()
	    {

	        using (var ctx = new PayrollDB())
	        {
	            try
	            {
	                var pl = from p in ctx.PayrollItems.Where(x => x.PayrollJobId == CurrentPayrollJob.PayrollJobId)
                            .OrderByDescending(x => x.IncomeDeduction)
	                        .ThenBy(x => x.PayrollSetupItem == null ? x.Priority : x.PayrollSetupItem.Priority)
	                    let pi = p.PayrollSetupItem == null ? p.Priority : p.PayrollSetupItem.Priority
	                    group p by new
	                    {
	                        pname = p.Name,
	                        p.CreditAccount.Institution.Name,
	                        p.IncomeDeduction,
	                        Priority = pi
	                    }
	                    into g //
	                    select new PayrollItemsReportModel.PayrollReportLine
	                    {
	                        InstitionName = g.Key.Name,
	                        PayrollItemName = g.Key.pname,
	                        Amount = g.Sum(p => p.Amount),
	                        //CreditAmount = g.Sum(p => p.CreditAmount),
	                        //DebitAmount = g.Sum(p => p.DebitAmount),
	                        Priority = g.Key.Priority,
	                        IncomeDeduction = g.Key.IncomeDeduction
	                    };


	                PayrollItems = new ObservableCollection<PayrollItemsReportModel.PayrollReportLine>(pl
	                    .OrderByDescending(x => x.IncomeDeduction).ThenBy(x => x.Priority)); //
	            }
	            catch (Exception exception)
	            {
	                Console.WriteLine(exception);
	                throw;
	            }
	        }

	    }

	    public double PayrollJobTotal
        {
            get
            {
                if (CurrentPayrollJob == null) return 0;
                return PayrollItems.Sum(pi => pi.Amount);
            }
        }

        ObservableCollection<PayrollItemsReportModel.PayrollReportLine> _payrollItems;
        public ObservableCollection<PayrollItemsReportModel.PayrollReportLine> PayrollItems
        {
            get
            {
                return _payrollItems;
            }
            set
            {
                _payrollItems = value;
                OnStaticPropertyChanged("PayrollItems");
            }
        }

        public class PayrollReportLine
        {
            public string PayrollItemName { get; set; }
            public int Priority { get; set; }
            public double Amount { get; set; }
            public bool IncomeDeduction { get; set; }
            //public double CreditAmount { get; set; }
            //public double DebitAmount { get; set; }
            public string InstitionName { get; set; }
            

        }
		
	}
}