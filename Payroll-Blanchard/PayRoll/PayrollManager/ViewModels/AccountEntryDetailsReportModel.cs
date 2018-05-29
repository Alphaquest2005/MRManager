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
	public class AccountEntryDetailsReportModel : BaseViewModel
	{
		public AccountEntryDetailsReportModel()
		{
            staticPropertyChanged += PaysubModel_staticPropertyChanged;
		}

        void PaysubModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentAccount" && CurrentAccount != null)
            {
                //MergeEmployeAccountIntoAccount(CurrentAccount);
                OnPropertyChanged(nameof(CurrentCreditEntries));
                OnPropertyChanged(nameof(CurrentDebitEntries));
                OnPropertyChanged(nameof(NetTotal));
                OnPropertyChanged(nameof(NetTotalCredit));
                OnPropertyChanged(nameof(NetTotalDebit));
            }
        }

	    public ObservableCollection<AccountEntrySummaryLine> CurrentCreditEntries
	    {
	        get
	        {
	            try
	            {
	                using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	                {
	                    if (BaseViewModel.Instance.CurrentPayrollJob == null)
	                        return new ObservableCollection<AccountEntrySummaryLine>();
	                    var res = ctx.Accounts.OfType<InstitutionAccounts>()
	                        .Where(x => x.Institution.InstitutionId == CurrentAccount.InstitutionId
	                                    && x.AccountId == CurrentAccount.AccountId)
	                        .OrderByDescending(x => x.AccountName)
	                        .SelectMany(x => x.AccountEntries.Where(z => z.CreditAmount != 0 && z.PayrollItem.PayrollJobId == BaseViewModel.Instance.CurrentPayrollJob.PayrollJobId).Select(z =>
	                            new AccountEntrySummaryLine
	                            {
	                                Payee = x.PayeeInstitution.Name,
	                                Account = z.Account,
	                                Total = z.DebitAmount - z.CreditAmount
	                            })).ToList();

	                    var empres = ctx.Accounts
	                        .Where(x => x.Institution.InstitutionId == CurrentAccount.InstitutionId
                                        
                                        && x.AccountId == CurrentAccount.AccountId
	                                    && x.EmployeeAccounts != null)
	                        .OrderByDescending(x => x.AccountName)
	                        .SelectMany(x => x.AccountEntries.Where(z => z.CreditAmount != 0 && z.PayrollItem.PayrollJobId == BaseViewModel.Instance.CurrentPayrollJob.PayrollJobId).Select(z =>
	                            new AccountEntrySummaryLine
	                            {
	                                Payee = x.EmployeeAccounts.Employee.FirstName + " " + x.EmployeeAccounts.Employee.LastName,
	                                Account = z.Account,
	                                Total = z.DebitAmount - z.CreditAmount
	                            })).ToList();
	                    res.AddRange(empres);

                        return new ObservableCollection<AccountEntrySummaryLine>(res);
                        //var lst = from i in ctx.AccountEntries
                        //        .Where(x => x.Account.Institution.InstitutionId == CurrentAccount.InstitutionId &&
                        //                     x.PayrollItem.PayrollJobId == BaseViewModel.Instance.CurrentPayrollJob.PayrollJobId
                        //                    && x.AccountId == CurrentAccount.AccountId)
                        //        .OrderBy(x => x.PayrollItem.Employee.LastName)
                        //        .ThenByDescending(x => x.PayrollItem.IncomeDeduction)
                        //        .ThenBy(x => x.PayrollItem.Priority)
                        //    group i by new { i.PayrollItem.Employee, i.Account }
                        //    into g
                        //    select new AccountEntrySummaryLine
                        //    {
                        //        Payee = (g.Key.Employee.FirstName + " " + g.Key.Employee.LastName),
                        //        Account = g.Key.Account,
                        //        Total = g.Sum(z => z.CreditAmount - z.DebitAmount) //
                        //    };

                        //var employeeAccountSummaryLines = lst as IList<AccountEntrySummaryLine> ??
                        //                                  new List<AccountEntrySummaryLine>();
                        //if (employeeAccountSummaryLines.Any())
                        //{
                        //    return new ObservableCollection<AccountEntrySummaryLine>(
                        //        employeeAccountSummaryLines.Where(
                        //            x => x != null && x.Account != null &&
                        //                 (x.Total > 0 && x.Account.AccountNumber != "1350-75"))); //
                        //}
                        //else
                        //{
                        //    return new ObservableCollection<AccountEntrySummaryLine>(); //
                        //}
                    }
	            }
	            catch (Exception e)
	            {
	                Console.WriteLine(e);
	                throw;
	            }


	        }
	    }

	    public ObservableCollection<AccountEntrySummaryLine> CurrentDebitEntries
	    {
	        get
	        {
	            try
	            {
	                using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
	                {
	                    if (BaseViewModel.Instance.CurrentPayrollJob == null)
	                        return new ObservableCollection<AccountEntrySummaryLine>();
	                    var res = ctx.Accounts.OfType<InstitutionAccounts>()
	                        .Where(x => x.Institution.InstitutionId == CurrentAccount.InstitutionId
	                                    && x.AccountId == CurrentAccount.AccountId)
	                        .OrderByDescending(x => x.AccountName)
	                        .SelectMany(x => x.AccountEntries.Where(z => z.DebitAmount != 0 && z.PayrollItem.PayrollJobId == BaseViewModel.Instance.CurrentPayrollJob.PayrollJobId).Select(z =>
	                            new AccountEntrySummaryLine
	                            {
	                                Payee = x.PayeeInstitution.Name,
	                                Account = z.Account,
	                                Total = z.DebitAmount - z.CreditAmount
	                            })).ToList();

	                    var empres = ctx.Accounts
	                        .Where(x => x.Institution.InstitutionId == CurrentAccount.InstitutionId
	                                    && x.AccountId == CurrentAccount.AccountId
                                        && x.EmployeeAccounts != null)
	                        .OrderByDescending(x => x.AccountName)
	                        .SelectMany(x => x.AccountEntries.Where(z => z.DebitAmount != 0 && z.PayrollItem.PayrollJobId == BaseViewModel.Instance.CurrentPayrollJob.PayrollJobId).Select(z =>
	                            new AccountEntrySummaryLine
	                            {
	                                Payee = x.EmployeeAccounts.Employee.FirstName + " " + x.EmployeeAccounts.Employee.LastName,
	                                Account = z.Account,
	                                Total = z.DebitAmount - z.CreditAmount
	                            })).ToList();
                        res.AddRange(empres);
                        return new ObservableCollection<AccountEntrySummaryLine>(res);
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
	            if (CurrentCreditEntries == null || BaseViewModel.Instance.CurrentPayrollJob == null) return 0;

	            return CurrentCreditEntries.Sum(ae => ae.Total) - CurrentDebitEntries.Sum(ae => ae.Total);
	        }
	    }

	    public double NetTotalCredit
	    {
	        get
	        {
	            if (BaseViewModel.Instance.CurrentPayrollJob == null) return 0;

	            return CurrentCreditEntries.Sum(ae => ae.Total);
	        }
	    }

	    public double NetTotalDebit
	    {
	        get
	        {
	            if (BaseViewModel.Instance.CurrentPayrollJob == null) return 0;

	            return CurrentDebitEntries.Sum(ae => ae.Total);
	        }
	    }

        public class AccountEntrySummaryLine
	    {
	        public string Payee { get; set; }
	        public double Total { get; set; }
	        public DataLayer.Account Account { get; set; }
	    }


    }
}