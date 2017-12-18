using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;
namespace PayrollManager
{
	/// <summary>
	/// Interaction logic for PayrollSetupSummary.xaml
	/// </summary>
	public partial class PayrollSetupSummary : UserControl
	{
		public PayrollSetupSummary()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		}




		private void ListBox_Drop_2(object sender, DragEventArgs e)
		{
			ListBox lstbox = ((ListBox)sender);
			foreach (DataLayer.PayrollSetupItem item in lstbox.Items)
			{
				item.Priority = lstbox.Items.IndexOf(item);
			}
		}

		private void AutoGenTxt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			BaseViewModel.Instance.SetupAllEmployees();
		}

		private void AddSelectedItem(object sender, SelectionChangedEventArgs e)
		{
			if (MultiSelectChk.IsChecked == false)
			{

				for (int i = 0; i < ItemsLst.SelectedItems.Count - 1; i++)
				{
					ItemsLst.SelectedItems.RemoveAt(i);
				}


			}
			BaseViewModel.Instance.CurrentSelectedPayrollSetups = new System.Collections.ObjectModel.ObservableCollection<DataLayer.PayrollSetupItem>(this.ItemsLst.SelectedItems.OfType<DataLayer.PayrollSetupItem>());
		}


	}
}