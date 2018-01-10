using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace PayrollManager.DataLayer
{
    public partial class Account
    {
        //private ObservableCollection<DataLayer.AccountEntry> _accountEntries = null;
        //public ObservableCollection<DataLayer.AccountEntry> AccountEntries
        //{
        //    get
        //    {
        //        if (_accountEntries == null)
        //        {
        //            return new ObservableCollection<AccountEntry>(RealAccountEntries);
        //        }
        //        return _accountEntries;
        //    }
        //}

        public double Total
        {
            get
            {
                if (CurrentAccountEntries == null || BaseViewModel.Instance.CurrentPayrollJob == null) return 0;

                return CurrentAccountEntries.Sum(ae => ae.CreditAmount - ae.DebitAmount);
            }
        }

        

        public double TotalDebit
        {
            get
            {
                if (CurrentAccountEntries == null || BaseViewModel.Instance.CurrentPayrollJob == null) return 0;

                return CurrentAccountEntries.Sum(ae => ae.DebitAmount);
            }
        }



        public double TotalCredit
        {
            get
            {
                if (CurrentAccountEntries == null || BaseViewModel.Instance.CurrentPayrollJob == null) return 0;

                return CurrentAccountEntries.Sum(ae => ae.CreditAmount);
            }
        }

       
        ObservableCollection<AccountEntry> _currentAccountEntries = new ObservableCollection<AccountEntry>();

        public ObservableCollection<AccountEntry> CurrentAccountEntries
        {
            get
            {

                if (_currentAccountEntries == null || !_currentAccountEntries.Any() ||
                    (BaseViewModel.Instance.CurrentPayrollJob != null && 
                        _currentAccountEntries.First().PayrollItem  != null &&
                        BaseViewModel.Instance.CurrentPayrollJob.PayrollJobId != _currentAccountEntries.First().PayrollItem.PayrollJobId))
                {
                    if ( BaseViewModel.Instance.CurrentPayrollJob == null ||
                        BaseViewModel.Instance.CurrentCompany == null) return null;
                    //return new ObservableCollection<AccountEntry>(AccountEntries.Where(a => a.PayrollItem.PayrollJobId == BaseViewModel.InstanceCurrentPayrollJob.PayrollJobId 
                    //                                                && a.PayrollItem.Employee.InstitutionId == BaseViewModel.InstanceCurrentCompany.InstitutionId 
                    //                                                && a.PayrollItem.PayrollJob.Branch != null 
                    //                                                && a.PayrollItem.PayrollJob.Branch.InstitutionId == BaseViewModel.InstanceCurrentCompany.InstitutionId)
                    //                                                .OrderByDescending(x => x.PayrollItem.IncomeDeduction).ThenBy(x => x.PayrollItem.Priority));
                    List<AccountEntry> alst;
                    using (var ctx = new PayrollDB())
                    {
                        alst =
                            ctx.AccountEntries
                                     .Where(a => a.PayrollItem.PayrollJobId == BaseViewModel.Instance.CurrentPayrollJob.PayrollJobId
                                                 && a.AccountId == AccountId
                                                 && a.PayrollItem.Employee.CompanyId == BaseViewModel.Instance.CurrentCompany.InstitutionId)
                                     .Include(x => x.PayrollItem)
                                     .OrderByDescending(x => x.PayrollItem.IncomeDeduction)
                                     .ThenBy(x => x.PayrollItem.Priority).ToList();

                    }
                    _currentAccountEntries = new ObservableCollection<AccountEntry>(alst);
                }
                return _currentAccountEntries;
            }
        }

        public ObservableCollection<PayrollSummaryLine> PayrollSummary
        {
            get
            {
                var plst = from p in CurrentAccountEntries
                           group p by p.PayrollItem.Name into g
                           select new PayrollSummaryLine
                           {
                               PayrollItemName = g.Key,
                               DebitAmount = g.Sum(x => x.DebitAmount),
                               CreditAmount = g.Sum(x => x.CreditAmount)
                           }
                           ;
                return new ObservableCollection<PayrollSummaryLine>(plst);
            }

        }

        public class PayrollSummaryLine
        {
            public string PayrollItemName { get; set; }
            public double DebitAmount { get; set; }
            public double CreditAmount { get; set; }


        }

        public ObservableCollection<DataLayer.AccountEntry> DebitEntries
        {
            get
            {
                if (CurrentAccountEntries == null || BaseViewModel.Instance.CurrentPayrollJob == null) return null;
                return new ObservableCollection<AccountEntry>(CurrentAccountEntries.Where(a => a.DebitAmount != 0));
            }
        }

        public ObservableCollection<DataLayer.AccountEntry> CreditEntries
        {
            get
            {
                if (CurrentAccountEntries == null || BaseViewModel.Instance.CurrentPayrollJob == null) return null;
                return new ObservableCollection<AccountEntry>(CurrentAccountEntries.Where(a => a.CreditAmount != 0 ));
            }
        }


    }
}
