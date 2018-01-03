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
    public class CompanyModel : BaseViewModel
    {




        public void DeleteCompany()
        {
            if (CurrentCompany == null) return;
            var res = MessageBox.Show(
                "Are you sure you want to Delete this Company?",
                "Delete Payroll Job", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.No) return;
            if (CurrentCompany.CompanyId != 0)
                using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
                {
                    ctx.Companies.Attach(CurrentCompany);
                    ctx.Companies.DeleteObject(CurrentCompany);
                    // db.PayrollItems.Detach(CurrentPayrollItem);


                    SaveDatabase(ctx);
                }
            CurrentCompany = null;
            OnStaticPropertyChanged("CurrentCompany");
            OnStaticPropertyChanged("Companies");

        }

        public void NewCompany()
        {
            DataLayer.Company newCompany;
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {

                newCompany = ctx.Companies.CreateObject<DataLayer.Company>();


                newCompany.Name = "Name";
                newCompany.ShortName = "Short Name";
                newCompany.Address = "Address";
                newCompany.PhoneNumber = "Phone Number";
                ctx.Companies.AddObject(newCompany);
                SaveDatabase(ctx);
            }
            CurrentCompany = newCompany;
            OnPropertyChanged("CurrentCompany");


        }


        public void SaveCompany()
        {
            var companyId = CurrentCompany.CompanyId;
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                if (CurrentCompany == null) return;

                if (CurrentCompany.CompanyId == 0)
                {
                    ctx.Companies.AddObject(CurrentCompany);
                }
                else
                {
                    if (CurrentCompany.EntityState == EntityState.Added) return;
                    var ritm = ctx.Companies.First(x => x.CompanyId == CurrentCompany.CompanyId);
                    ctx.Companies.Attach(ritm);
                    ctx.Companies.ApplyCurrentValues(CurrentCompany);
                }

                SaveDatabase(ctx);
            }

            OnStaticPropertyChanged("Companies");
            CycleCurrentCompany(companyId);
        }
    }
}