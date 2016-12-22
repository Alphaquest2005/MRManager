using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class SettingsList_AutoGen
	{
		public SettingsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("SettingsViewModelDataSource") as SettingsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		SettingsViewModel_AutoGen im;

	}
}
