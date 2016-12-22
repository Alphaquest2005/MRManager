using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ProcessStepScreens.xaml
	/// </summary>
	public partial class ProcessStepScreensList_AutoGen
	{
		public ProcessStepScreensList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ProcessStepScreensViewModelDataSource") as ProcessStepScreensViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ProcessStepScreensViewModel_AutoGen im;

	}
}
