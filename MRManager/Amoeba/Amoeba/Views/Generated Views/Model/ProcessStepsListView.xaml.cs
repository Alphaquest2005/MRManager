using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ProcessSteps.xaml
	/// </summary>
	public partial class ProcessStepsList_AutoGen
	{
		public ProcessStepsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ProcessStepsViewModelDataSource") as ProcessStepsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ProcessStepsViewModel_AutoGen im;

	}
}
