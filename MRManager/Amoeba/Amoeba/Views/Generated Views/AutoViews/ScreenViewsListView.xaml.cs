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
	public partial class ScreenViewsAutoViewList_AutoGen
	{
		public ScreenViewsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ScreenViewsAutoViewModelDataSource") as ScreenViewsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ScreenViewsAutoViewModel_AutoGen im;

	}
}
