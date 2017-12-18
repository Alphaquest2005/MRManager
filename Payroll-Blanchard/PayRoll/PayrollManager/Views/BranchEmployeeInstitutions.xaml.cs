using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PayrollManager.Converters;
using PayrollManager.Views;
using SUT.PrintEngine.Utils;

namespace PayrollManager
{
    /// <summary>
    /// Interaction logic for BranchPayrollItemBreakDown.xaml
    /// </summary>
    public partial class BranchEmployeeInstitutions : UserControl
    {
        public BranchEmployeeInstitutions()
        {
            this.InitializeComponent();
            im = (BranchEmployeeInstitutionsModel)this.FindResource("BranchEmployeeInstitutionsModelDataSource");
            im.DeductionsGrid = DeductionsGrid;
            im.NetSalaryGrid = NetSalaryGrid;
            im.GrandTotalGrid = GrandTotalGrid;
            // Insert code required on object creation below this point.
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(DeductionsGrid, DeductionsGrid_ItemSourceChanged);
                dpd.AddValueChanged(NetSalaryGrid, NetSalaryGrid_ItemSourceChanged);
                dpd.AddValueChanged(DeductionsGrid, GrandTotalGrid_ItemSourceChanged);
                dpd.AddValueChanged(NetSalaryGrid, GrandTotalGrid_ItemSourceChanged);
                dpd.AddValueChanged(GrandTotalGrid, GrandTotalGrid_ItemSourceChanged);
            }
        }

        private void GrandTotalGrid_ItemSourceChanged(object sender, EventArgs e)
        {
            im.PopulateGrandTotalGrid();
        }

        private void NetSalaryGrid_ItemSourceChanged(object sender, EventArgs e)
        {
            im.PopulateNetSalaryGrid();
        }

        private void DeductionsGrid_ItemSourceChanged(object sender, EventArgs e)
        {
            im.PopulateDeductionsGrid();
        }
        BranchEmployeeInstitutionsModel im;
        private void PrintDeductions(object sender, MouseButtonEventArgs e)
        {
            //FrameworkElement rpt = (FrameworkElement)DailyReportGD;
           // WPF2PDF.CreateAndOpenPDF(ref DailyReportGD, "BranchEmployeeInstitutions");
            //if (DeductionsGrid.PrintCommand.CanExecute(DeductionsGrid))
            //DeductionsGrid.PrintCommand.Execute(DeductionsGrid);

            FrameworkElement rpt = (FrameworkElement)DailyReportGD;
            PrintClass.Print(ref rpt);
        }




        private void GridData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.IsNewItem == false)
            {
                if ((e.Row.Item as BranchEmployeeInstitutionsModel.EmployeeSummaryLine).Employee == "Total")
                {
                    // e.Row.Background = new SolidColorBrush(Colors.DodgerBlue);
                    // e.Row.FontSize = 14;
                    e.Row.FontWeight = FontWeights.Bold;

                }
            }
        }

        private void NetSalary_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.IsNewItem == false)
            {
                if ((e.Row.Item as BranchEmployeeInstitutionsModel.EmployeeAccountSummaryLine).Employee == "Total")
                {
                    // e.Row.Background = new SolidColorBrush(Colors.DodgerBlue);
                    // e.Row.FontSize = 14;
                    e.Row.FontWeight = FontWeights.Bold;

                }
            }
        }

        private void PrintNetSalary(object sender, MouseButtonEventArgs e)
        {
            WPF2PDF.CreateAndOpenPDF(ref NetSalaryGrd, "Net Salary");
        }

        private void Deductions2Excel(object sender, MouseButtonEventArgs e)
        {
            var p = new ExportToExcel();
            p.GenerateReport(DeductionsGrid);
        }

        private void NetSalary2Excel(object sender, MouseButtonEventArgs e)
        {
            var p = new ExportToExcel();
            p.GenerateReport(NetSalaryGrid);
        }

        private void GrandTotal_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.FontWeight = FontWeights.Bold;
        }

        private void GrandTotal2Excel(object sender, MouseButtonEventArgs e)
        {
            var p = new ExportToExcel();
            p.GenerateReport(GrandTotalGrid);
        }
    }
}