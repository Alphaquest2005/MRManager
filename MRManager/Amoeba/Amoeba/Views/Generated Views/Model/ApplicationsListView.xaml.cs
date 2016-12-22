using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Applications.xaml
	/// </summary>
	public partial class ApplicationsList_AutoGen
	{
		public ApplicationsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ApplicationsViewModelDataSource") as ApplicationsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ApplicationsViewModel_AutoGen im;

	}
}
