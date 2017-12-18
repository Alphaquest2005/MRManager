using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Linq;
using PayrollManager.DataLayer;

namespace PayrollManager
{
    public class PayrollJobModel : BaseViewModel
    {
       



        public void DeletePayrollJob()
        {
            if (CurrentPayrollJob == null) return;
            var res = MessageBox.Show(
                "Are you sure you want to Delete this payroll Job? Have you checked all 'One Off' Payroll items?",
                "Delete Payroll Job", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.No) return;
            if(CurrentPayrollJob.PayrollJobId != 0)
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                ctx.PayrollJobs.Attach(CurrentPayrollJob);
                ctx.PayrollJobs.DeleteObject(CurrentPayrollJob);
                // db.PayrollItems.Detach(CurrentPayrollItem);


                SaveDatabase(ctx);
            }
            CurrentPayrollJob = null;
            UpdatePayrollJobs();
            OnStaticPropertyChanged("CurrentPayrollJob");
            OnStaticPropertyChanged("PayrollJobs");

        }

        public void NewPayrollJob()
        {
            DataLayer.PayrollJob newemp;
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {

                newemp = ctx.PayrollJobs.CreateObject<DataLayer.PayrollJob>();
                newemp.BranchId = CurrentBranch.BranchId;
                newemp.PayrollJobType = ctx.PayrollJobTypes.FirstOrDefault();
                newemp.StartDate = DateTime.Now;
                newemp.EndDate = DateTime.Now;
                newemp.PaymentDate = DateTime.Now;
                newemp.PreparedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name; 
                newemp.Status = "Open";
                ctx.PayrollJobs.AddObject(newemp);
                SaveDatabase(ctx);
            }
            CurrentPayrollJob = newemp;
            OnPropertyChanged("CurrentPayrollJob");


        }


    }
}