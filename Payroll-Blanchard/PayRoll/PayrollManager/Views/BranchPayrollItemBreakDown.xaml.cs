using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PayrollManager.Converters;

namespace PayrollManager
{
	/// <summary>
	/// Interaction logic for BranchPayrollItemBreakDown.xaml
	/// </summary>
	public partial class BranchPayrollItemBreakDown : UserControl
	{
		public BranchPayrollItemBreakDown()
		{
			this.InitializeComponent();
            im = (BranchPayrollItemBreakDownModel)this.FindResource("BranchPayrollItemBreakDownModelDataSource");
            im.MyDataGrid = GridData;
			// Insert code required on object creation below this point.
            var dpd = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (dpd != null)
            {
                dpd.AddValueChanged(GridData, ItemSourceChanged);
            }
		}

        private void ItemSourceChanged(object sender, EventArgs e)
        {
           im.PopulateGrid();
        }
        BranchPayrollItemBreakDownModel im;
        private void PrintReport(object sender, MouseButtonEventArgs e)
        {

            // WPF2PDF.CreateAndOpenPDF(ref DailyReportGD, "PayrollItemBreakDown");
            FrameworkElement rpt = (FrameworkElement)DailyReportGD;
            PrintClass.Print(ref rpt);

        }

        private void GridData_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.IsNewItem == false)
            {
                if ((e.Row.Item as BranchPayrollItemBreakDownModel.BranchPayrollItemSummaryLine).Payroll_Item == "Total")
                {
                    // e.Row.Background = new SolidColorBrush(Colors.DodgerBlue);
                    // e.Row.FontSize = 14;
                    e.Row.FontWeight = FontWeights.Bold;

                }
            }
        }

        private void ExcelReport(object sender, MouseButtonEventArgs e)
        {
            var p = new ExportToExcel();
            p.GenerateReport(GridData);
        }
	}
}