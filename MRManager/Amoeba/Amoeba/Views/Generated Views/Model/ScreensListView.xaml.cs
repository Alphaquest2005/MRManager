using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Screens.xaml
	/// </summary>
	public partial class ScreensList_AutoGen
	{
		public ScreensList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ScreensViewModelDataSource") as ScreensViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ScreensViewModel_AutoGen im;

	}
}
