using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using System.Data.Entity;
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
            if (CurrentCompany.InstitutionId != 0)
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
                newCompany.Institution = ctx.Institutions.CreateObject<DataLayer.Institution>();

                newCompany.Institution.Name = "Name";
                newCompany.Institution.ShortName = "Short Name";
                newCompany.Institution.Address = "Address";
                newCompany.Institution.PhoneNumber = "Phone Number";
                ctx.Companies.AddObject(newCompany);
                SaveDatabase(ctx);
            }
            CurrentCompany = newCompany;
            OnPropertyChanged("CurrentCompany");


        }


        public void SaveCompany()
        {
            if (CurrentCompany == null) return;
            var institutionId = CurrentCompany.InstitutionId;
            using (var ctx = new PayrollDB(Properties.Settings.Default.PayrollDB))
            {
                if (CurrentCompany == null) return;

                if (CurrentCompany.InstitutionId == 0)
                {
                    ctx.Companies.AddObject(CurrentCompany);
                }
                else
                {
                    if (CurrentCompany.EntityState == System.Data.EntityState.Added) return;
                    var ritm = ctx.Institutions.First(x => x.InstitutionId == CurrentCompany.InstitutionId);
                    ctx.Institutions.Attach(ritm);
                    ctx.Institutions.ApplyCurrentValues(CurrentCompany.Institution);


                }

                SaveDatabase(ctx);
            }
            LoadCompanies();
            OnStaticPropertyChanged("Companies");
            CycleCurrentCompany(institutionId);
        }
    }
}