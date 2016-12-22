using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ApplicationSetting.xaml
	/// </summary>
	public partial class ApplicationSettingList_AutoGen
	{
		public ApplicationSettingList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ApplicationSettingViewModelDataSource") as ApplicationSettingViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ApplicationSettingViewModel_AutoGen im;

	}
}
