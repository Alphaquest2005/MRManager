using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.ObjectModel;
using PayrollManager.DataLayer;

namespace PayrollManager
{
	public class PaysubModel : BaseViewModel
	{
		public PaysubModel()
		{
            staticPropertyChanged += PaysubModel_staticPropertyChanged;
		}

        void PaysubModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentInstitutionAccount" && CurrentInstitutionAccount != null)
            {
                //MergeEmployeAccountIntoInstitutionAccount(CurrentInstitutionAccount);
                OnPropertyChanged(nameof(CurrentEmpNetCreditEntries));
                OnPropertyChanged(nameof(CurrentEmpNetDebitEntries));
                OnPropertyChanged(nameof(NetTotal));
                OnPropertyChanged(nameof(NetTotalCredit));
                OnPropertyChanged(nameof(NetTotalDebit));
            }
        }

	    public ObservableCollection<EmployeeAccountSummaryLine> CurrentEmpNetCreditEntries
	    {
	        get
	        {
	            try
	            {
	                using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	                {
	                    var lst = from i in ctx.AccountEntries
	                            .Where(x => x.Accounts.Institution.InstitutionId == CurrentInstitutionAccount.InstitutionId &&
	                                         x.PayrollItem.PayrollJobId == BaseViewModel.Instance.CurrentPayrollJob.PayrollJobId
	                                        && x.AccountId == CurrentInstitutionAccount.AccountId)
	                            .OrderBy(x => x.PayrollItem.Employee.LastName)
	                            .ThenByDescending(x => x.PayrollItem.IncomeDeduction)
	                            .ThenBy(x => x.PayrollItem.Priority)
	                        group i by new { i.PayrollItem.Employee, i.Accounts }
	                        into g
	                        select new EmployeeAccountSummaryLine
	                        {
	                            Employee = (g.Key.Employee.FirstName + " " + g.Key.Employee.LastName),
	                            Account = g.Key.Accounts,
	                            Total = g.Sum(z => z.CreditAmount - z.DebitAmount) //
	                        };

	                    var employeeAccountSummaryLines = lst as IList<EmployeeAccountSummaryLine> ??
	                                                      new List<EmployeeAccountSummaryLine>();
	                    if (employeeAccountSummaryLines.Any())
	                    {
	                        return new ObservableCollection<EmployeeAccountSummaryLine>(
	                            employeeAccountSummaryLines.Where(
	                                x => x != null && x.Account != null &&
	                                     (x.Total > 0 && x.Account.AccountNumber != "1350-75"))); //
	                    }
	                    else
	                    {
	                        return new ObservableCollection<EmployeeAccountSummaryLine>(); //
	                    }
	                }
	            }
	            catch (Exception e)
	            {
	                Console.WriteLine(e);
	                throw;
	            }


	        }
	    }

	    public ObservableCollection<EmployeeAccountSummaryLine> CurrentEmpNetDebitEntries
	    {
	        get
	        {
	            try
	            {
	                using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	                {
	                    if (BaseViewModel.Instance.CurrentPayrollJob == null)
	                        return new ObservableCollection<EmployeeAccountSummaryLine>();
	                    var lst = from i in ctx.AccountEntries
	                            .Where(x => x.Accounts.Institution.InstitutionId == CurrentInstitutionAccount.InstitutionId &&
	                                        x.PayrollItem.PayrollJobId == BaseViewModel.Instance.CurrentPayrollJob.PayrollJobId
	                                        && x.AccountId == CurrentInstitutionAccount.AccountId)
                                .OrderByDescending(x => x.PayrollItem.IncomeDeduction)
	                            .ThenBy(x => x.PayrollItem.Priority)

	                        group i by new { i.PayrollItem.Employee, i.Accounts }
	                        into g
	                        select new EmployeeAccountSummaryLine
	                        {
	                            Employee = (g.Key.Employee.FirstName + " " + g.Key.Employee.LastName),
	                            Account = g.Key.Accounts,
	                            Total = g.Sum(z => z.DebitAmount - z.CreditAmount)
	                        };

	                    var res = lst.ToList();//.Where(x => x.Total > 0)

	                    return new ObservableCollection<EmployeeAccountSummaryLine>(res);
	                }
	            }
	            catch (Exception e)
	            {
	                Console.WriteLine(e);
	                throw;
	            }
	        }
	    }

	    public double NetTotal
	    {
	        get
	        {
	            if (CurrentEmpNetCreditEntries == null || BaseViewModel.Instance.CurrentPayrollJob == null) return 0;

	            return CurrentEmpNetCreditEntries.Sum(ae => ae.Total) - CurrentEmpNetDebitEntries.Sum(ae => ae.Total);
	        }
	    }

	    public double NetTotalCredit
	    {
	        get
	        {
	            if (BaseViewModel.Instance.CurrentPayrollJob == null) return 0;

	            return CurrentEmpNetCreditEntries.Sum(ae => ae.Total);
	        }
	    }

	    public double NetTotalDebit
	    {
	        get
	        {
	            if (BaseViewModel.Instance.CurrentPayrollJob == null) return 0;

	            return CurrentEmpNetDebitEntries.Sum(ae => ae.Total);
	        }
	    }

        public class EmployeeAccountSummaryLine
	    {
	        public string Employee { get; set; }
	        public double Total { get; set; }
	        public DataLayer.Account Account { get; set; }
	    }


    }
}