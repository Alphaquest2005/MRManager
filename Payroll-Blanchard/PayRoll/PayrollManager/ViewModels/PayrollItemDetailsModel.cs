using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using EmailLogger;
using PayrollManager.DataLayer;
using EntityState = System.Data.EntityState;

namespace PayrollManager
{
    public class PayrollItemDetailsModel : BaseViewModel
    {
        public PayrollItemDetailsModel()
        {
            staticPropertyChanged += PayrollItemDetailsModel_staticPropertyChanged;


        }

        void PayrollItemDetailsModel_staticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
            Debug.WriteLine(e.PropertyName);
            if ((e.PropertyName.Contains("Account") && e.PropertyName != "CurrentAccountsLst") || (e.PropertyName == "CurrentEmployee") )
            {
                UpdateCurrentAccountsLst();
                OnStaticPropertyChanged("CurrentAccountsLst");
            }
            if (e.PropertyName == nameof(CurrentEmployee))
            {
                if (CurrentEmployee == null)
                {
                    CurrentPayrollItems = new List<PayrollItem>();
                }
                else
                {
                    using (var ctx = new PayrollDB())
                    {
                        CurrentPayrollItems = ctx.PayrollItems.Where(x => x.EmployeeId == CurrentEmployee.EmployeeId)
                            .ToList();
                    }
                }

            }
        }

        public void SavePayrollItem()
        {

            using (var ctx = new PayrollDB())
            {
                if(CurrentPayrollItem == null) return;
                if (CurrentPayrollItem.PayrollItemId == 0)
                {
                    ctx.PayrollItems.AddObject(CurrentPayrollItem);
                }
                else
                {
                    if (CurrentPayrollItem.EntityState == EntityState.Added) return;
                    var ritm = ctx.PayrollItems.Single(x => x.PayrollItemId == CurrentPayrollItem.PayrollItemId);
                    ctx.PayrollItems.Attach(ritm);
                    ctx.PayrollItems.ApplyCurrentValues(CurrentPayrollItem);
                }
                
                SaveDatabase(ctx);

                CurrentPayrollItems = ctx.PayrollItems.Include(x => x.PayrollSetupItem).Where(x => x.EmployeeId == CurrentEmployee.EmployeeId && x.PayrollJobId == CurrentPayrollJob.PayrollJobId)
                    .ToList();
                UpdatePayrollItemsBaseAmounts(CurrentPayrollItems, ctx);

                
            }
            

            OnStaticPropertyChanged("CurrentPayrollJob");
            OnStaticPropertyChanged("PayrollItems");
            OnStaticPropertyChanged("Employees");
            OnStaticPropertyChanged("CurrentPayrollItem");


        }

        public List<PayrollItem> CurrentPayrollItems { get; set; }

        public void DeletePayrollItem()
        {
            if (CurrentPayrollItem == null) return;
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {

                if (CurrentPayrollItem.PayrollItemId!= 0)
                {
                    var ritm = ctx.PayrollItems.First(x => x.PayrollItemId == CurrentPayrollItem.PayrollItemId);
                    foreach (var item in ctx.AccountEntries.Where(x => x.PayrollItemId == CurrentPayrollItem.PayrollItemId).ToList())
                    {
                        ctx.AccountEntries.DeleteObject(item);
                    }
                    ctx.PayrollItems.DeleteObject(ritm);
                    SaveDatabase(ctx);

                    CurrentPayrollItems = ctx.PayrollItems.Include(x => x.PayrollSetupItem).Where(x => x.EmployeeId == CurrentEmployee.EmployeeId && x.PayrollJobId == CurrentPayrollJob.PayrollJobId)
                        .ToList();
                    UpdatePayrollItemsBaseAmounts(CurrentPayrollItems, ctx);
                   
                }
                CurrentPayrollItem = null;
                OnStaticPropertyChanged("CurrentPayrollJob");
                OnStaticPropertyChanged("PayrollItems");
                OnStaticPropertyChanged("Employees");
                OnStaticPropertyChanged("CurrentPayrollItem");
            }
        }

        //[MyExceptionHandlerAspect]

        public static void UpdatePayrollItemsBaseAmounts(List<PayrollItem> payrollItems, PayrollDB ctx)
        {
            try
            {
                var incomeAmts = payrollItems.Where(p => p.IncomeDeduction == true && !p.ApplyToTaxableBenefits.GetValueOrDefault() && p.ParentPayrollItem == null && ((p.PayrollSetupItem != null && p.PayrollSetupItem.Name != "Salary") || p.PayrollSetupItem == null))
                    ?.Sum(p => p.Amount);

                var taxableBenefitsAmts = payrollItems.Where(p => p.IncomeDeduction == true && p.ApplyToTaxableBenefits.GetValueOrDefault() && p.ParentPayrollItem == null && ((p.PayrollSetupItem != null && p.PayrollSetupItem.Name != "Salary") || p.PayrollSetupItem == null))
                    ?.Sum(p => p.Amount);

                var salaryItm = payrollItems.First(p => p.IncomeDeduction == true && p.ParentPayrollItem == null &&
                                                        p.PayrollSetupItem != null &&
                                                          p.PayrollSetupItem.Name == "Salary").PayrollSetupItemId;
                var salary =
                    ctx.PayrollEmployeeSetup.First(
                        x => x.PayrollSetupItemId == salaryItm && x.EmployeeId ==
                             BaseViewModel.Instance.CurrentEmployee.EmployeeId).BaseAmount.GetValueOrDefault();
                foreach (var itm in payrollItems)
                {
                    if (itm.PayrollSetupItem == null)
                    {
                        itm.BaseAmount = (itm.ApplyToTaxableBenefits == true
                            ? salary + taxableBenefitsAmts.GetValueOrDefault() + incomeAmts.GetValueOrDefault()
                            : salary + incomeAmts.GetValueOrDefault());
                    }
                    else
                    {
                        itm.BaseAmount = (itm.PayrollSetupItem.ApplyToTaxableBenefits == true
                        ? salary + taxableBenefitsAmts.GetValueOrDefault() + incomeAmts.GetValueOrDefault()
                        : salary + incomeAmts.GetValueOrDefault());
                    }
                    
                    itm.Amount = Math.Abs(GetPayrollAmount(itm.BaseAmount, itm).GetValueOrDefault());
                    var dbItm = ctx.PayrollItems.First(x => x.PayrollItemId == itm.PayrollItemId);
                    dbItm.BaseAmount = itm.BaseAmount;
                    dbItm.Amount = itm.Amount;
                }
                SaveDatabase(ctx);
                BaseViewModel.Instance.GetEmployees();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void NewPayrollItem()
        {
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                DataLayer.PayrollItem newpi = ctx.PayrollItems.CreateObject();
                //ctx.PayrollJobs.Attach(CurrentPayrollJob);
                //newpi.PayrollJob = CurrentPayrollJob;
                newpi.EmployeeId = CurrentEmployee.EmployeeId;
                newpi.PayrollJobId = CurrentPayrollJob.PayrollJobId;
                ctx.PayrollItems.AddObject(newpi);
                newpi.Status = "Amounts Processed";
                CurrentPayrollItem = newpi;
                OnPropertyChanged("CurrentPayrollItem");
                SaveDatabase(ctx);
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