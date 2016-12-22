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
	public partial class ScreensAutoViewList_AutoGen
	{
		public ScreensAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ScreensAutoViewModelDataSource") as ScreensAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ScreensAutoViewModel_AutoGen im;

	}
}
