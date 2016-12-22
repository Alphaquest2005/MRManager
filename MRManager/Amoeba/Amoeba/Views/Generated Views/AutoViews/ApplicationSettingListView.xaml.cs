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
	public partial class ApplicationSettingAutoViewList_AutoGen
	{
		public ApplicationSettingAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ApplicationSettingAutoViewModelDataSource") as ApplicationSettingAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ApplicationSettingAutoViewModel_AutoGen im;

	}
}
