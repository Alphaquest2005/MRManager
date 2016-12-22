using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ScreenViews.xaml
	/// </summary>
	public partial class ScreenViewsList_AutoGen
	{
		public ScreenViewsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ScreenViewsViewModelDataSource") as ScreenViewsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ScreenViewsViewModel_AutoGen im;

	}
}
