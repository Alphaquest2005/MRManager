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
	public partial class ScreenLayoutsAutoViewList_AutoGen
	{
		public ScreenLayoutsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ScreenLayoutsAutoViewModelDataSource") as ScreenLayoutsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ScreenLayoutsAutoViewModel_AutoGen im;

	}
}
