using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ScreenLayouts.xaml
	/// </summary>
	public partial class ScreenLayoutsList_AutoGen
	{
		public ScreenLayoutsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ScreenLayoutsViewModelDataSource") as ScreenLayoutsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ScreenLayoutsViewModel_AutoGen im;

	}
}
