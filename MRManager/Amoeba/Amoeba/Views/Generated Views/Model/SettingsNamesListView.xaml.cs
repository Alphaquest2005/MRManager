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
	public partial class SettingsNamesList_AutoGen
	{
		public SettingsNamesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("SettingsNamesViewModelDataSource") as SettingsNamesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		SettingsNamesViewModel_AutoGen im;

	}
}
