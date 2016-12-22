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
	public partial class ApplicationsAutoViewList_AutoGen
	{
		public ApplicationsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ApplicationsAutoViewModelDataSource") as ApplicationsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ApplicationsAutoViewModel_AutoGen im;

	}
}
