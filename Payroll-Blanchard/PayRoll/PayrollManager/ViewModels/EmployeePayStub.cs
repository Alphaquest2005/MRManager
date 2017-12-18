using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PayrollManager.DataLayer;

namespace PayrollManager
{
   public class EmployeePayStubModel : BaseViewModel
    {

       public EmployeePayStubModel()
       {
           staticPropertyChanged +=EmployeePayStubModel_staticPropertyChanged;
       }

       private void EmployeePayStubModel_staticPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
       {
           if (e.PropertyName == "CurrentEmployee")
           {
              OnStaticPropertyChanged("IncomePayrollLineItems");
              OnStaticPropertyChanged("DeductionPayrollLineItems");
           }
       }

       public double CurrentIncome
       {
           get
           {
               //return db.AccountEntries.Where(ae => ae.IncomeDeduction == true && ae.PayrollItem.PayrollJobId == BaseViewModel.CurrentPayrollJob.PayrollJobId && ae.PayrollItem.Employee == CurrentEmployee).Sum(ae => ae.Total);
               return CurrentIncomeAccountEntries.Sum(ae => ae.Total);
           }
       }

       public ObservableCollection<DataLayer.AccountEntry> CurrentIncomeAccountEntries
       {
           get
           {
               using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
               {
                   return new ObservableCollection<DataLayer.AccountEntry>(ctx.AccountEntries.Where(
                       ae => ae.PayrollItem.IncomeDeduction == true &&
                             ae.PayrollItem.PayrollJobId == CurrentPayrollJob.PayrollJobId &&
                             ae.PayrollItem.Employee == CurrentEmployee));
               }
           }
       }

       public ObservableCollection<DataLayer.AccountEntry> YearsIncomeAccountEntries
       {
           get
           {
               using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
               {
                   return new ObservableCollection<DataLayer.AccountEntry>(ctx.AccountEntries.Where(
                       ae => ae.PayrollItem.IncomeDeduction == true &&
                             (ae.PayrollItem.PayrollJob.EndDate.Year == CurrentPayrollJob.EndDate.Year &&
                              ae.PayrollItem.PayrollJob.EndDate <= EntityFunctions.AddHours(CurrentPayrollJob.EndDate,23)
                             ) && ae.PayrollItem.Employee == CurrentEmployee));
               }
           }
       }

       public ObservableCollection<DataLayer.AccountEntry> YearsDeductionAccountEntries
       {
           get
           {
               using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
               {
                   return new ObservableCollection<DataLayer.AccountEntry>(ctx.AccountEntries.Where(
                       ae => ae.PayrollItem.IncomeDeduction == false &&
                             (ae.PayrollItem.PayrollJob.EndDate.Year == CurrentPayrollJob.EndDate.Year &&
                              ae.PayrollItem.PayrollJob.EndDate <= EntityFunctions.AddHours(CurrentPayrollJob.EndDate,23)
                             ) && ae.PayrollItem.Employee == CurrentEmployee));
               }
           }
       }

       public double YearsIncome
       {
           get
           {
               return YearsIncomeAccountEntries.Sum(ae => ae.Total);
           }
       }

       public class PayrollSummaryLineItem
       {
         public string LineItemDescription{get;set;}
         public  double? CurrentAmount{get;set;}
         public  double? YearAmount{get;set;}
       }

        public IEnumerable<PayrollSummaryLineItem> IncomePayrollLineItems
        {
            get
            {
                try
                {
                    if (CurrentPayrollJob == null || CurrentEmployee == null) return null;
                    using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
                    {
                        var lst = from p in ctx.AccountEntries
                            where p.PayrollItem.IncomeDeduction == true &&
                                  (p.PayrollItem.PayrollJob.EndDate.Year ==
                                   CurrentPayrollJob.EndDate.Year &&
                                   p.PayrollItem.PayrollJob.EndDate <=
                                   EntityFunctions.AddHours(CurrentPayrollJob.EndDate,23)
                                  ) && p.PayrollItem.EmployeeId == CurrentEmployee.EmployeeId &&
                                  p.PayrollItem.ParentPayrollItem == null
                            group p by p.PayrollItem.Name
                            into pname
                            select new PayrollSummaryLineItem
                            {
                                LineItemDescription = pname.Key,
                                CurrentAmount = pname
                                    .Where(z => z.PayrollItem.PayrollJobId ==
                                                CurrentPayrollJob.PayrollJobId)
                                    .Sum(z => z.CreditAmount),
                                YearAmount = pname.Sum(z => z.CreditAmount)
                            };

                        List<PayrollSummaryLineItem> f = new List<PayrollSummaryLineItem>(lst.ToList());
                        f.Add(new PayrollSummaryLineItem()
                        {
                            LineItemDescription = "Total",
                            CurrentAmount = lst.Sum(p => p.CurrentAmount),
                            YearAmount = lst.Sum(p => p.YearAmount)
                        });

                        return f;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }


            }
        }

        public IEnumerable<PayrollSummaryLineItem> DeductionPayrollLineItems
        {
            get
            {
                try
                {
                    if (CurrentPayrollJob == null || CurrentEmployee == null) return null;
                    using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
                    {
                        if (CurrentPayrollJob == null) return null;
                        var lst = from p in ctx.AccountEntries
                            where p.PayrollItem.IncomeDeduction == false &&
                                  (p.PayrollItem.PayrollJob.EndDate.Year ==
                                   CurrentPayrollJob.EndDate.Year &&
                                   p.PayrollItem.PayrollJob.EndDate <=
                                   EntityFunctions.AddHours(CurrentPayrollJob.EndDate, 23)
                                  ) && p.PayrollItem.EmployeeId == CurrentEmployee.EmployeeId &&
                                  p.PayrollItem.ParentPayrollItem == null
                            group p by p.PayrollItem.Name
                            into pname
                            select new PayrollSummaryLineItem
                            {
                                LineItemDescription = pname.Key,
                                CurrentAmount = (double?)pname
                                    .Where(z => z.PayrollItem.PayrollJobId ==
                                                CurrentPayrollJob.PayrollJobId)
                                    .Sum(z => z.DebitAmount),
                                YearAmount = (double?)pname.Sum(z => z.DebitAmount)
                            };
                        List<PayrollSummaryLineItem> f = new List<PayrollSummaryLineItem>(lst.ToList());
                        f.Add(new PayrollSummaryLineItem()
                        {
                            LineItemDescription = "Total",
                            CurrentAmount = lst.Sum(p => p.CurrentAmount),
                            YearAmount = lst.Sum(p => p.YearAmount)
                        });
                        return f;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
        }


        internal void EmailReport(ref Grid rpt)
       {
           using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
           {
               DataLayer.EmailTemplate etmp = ctx.EmailTemplate.FirstOrDefault(et => et.Key == "EmployeePayStub");
               if (CurrentEmployee.EmailAddress == null)
               {
                   MessageBox.Show("Please add employee's email address before proceding");
                   return;
               }
               if (etmp != null)
               {
                   string pdffile = WPF2PDF.CreatePDF(ref rpt,CurrentEmployee.DisplayName.Replace(" ", "-") + "-" + "PayStub");

                   MyOutlook.Mail.CreateDraft(etmp.FromEmailAddress, etmp.Subject, etmp.EmailBody,
                       CurrentEmployee.EmailAddress, pdffile);
               }
               else
               {
                   MessageBox.Show("No email template found");
                   return;
               }
           }
       }


    }
}
