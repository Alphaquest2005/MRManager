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
	public partial class ScreenPartsList_AutoGen
	{
		public ScreenPartsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ScreenPartsViewModelDataSource") as ScreenPartsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ScreenPartsViewModel_AutoGen im;

	}
}
