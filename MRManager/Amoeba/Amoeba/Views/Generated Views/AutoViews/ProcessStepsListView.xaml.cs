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
	public partial class ProcessStepsAutoViewList_AutoGen
	{
		public ProcessStepsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ProcessStepsAutoViewModelDataSource") as ProcessStepsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ProcessStepsAutoViewModel_AutoGen im;

	}
}
