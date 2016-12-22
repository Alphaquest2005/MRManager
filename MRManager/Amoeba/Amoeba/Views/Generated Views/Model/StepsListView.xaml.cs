using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Steps.xaml
	/// </summary>
	public partial class StepsList_AutoGen
	{
		public StepsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("StepsViewModelDataSource") as StepsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		StepsViewModel_AutoGen im;

	}
}
