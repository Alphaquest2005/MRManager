using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ScreenParts.xaml
	/// </summary>
	public partial class ScreenPartsAutoViewList_AutoGen
	{
		public ScreenPartsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ScreenPartsAutoViewModelDataSource") as ScreenPartsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ScreenPartsAutoViewModel_AutoGen im;

	}
}
