using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for SettingsNames.xaml
	/// </summary>
	public partial class SettingsNamesAutoViewList_AutoGen
	{
		public SettingsNamesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("SettingsNamesAutoViewModelDataSource") as SettingsNamesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		SettingsNamesAutoViewModel_AutoGen im;

	}
}
