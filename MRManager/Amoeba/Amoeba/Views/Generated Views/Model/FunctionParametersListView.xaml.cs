using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for FunctionParameters.xaml
	/// </summary>
	public partial class FunctionParametersList_AutoGen
	{
		public FunctionParametersList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionParametersViewModelDataSource") as FunctionParametersViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionParametersViewModel_AutoGen im;

	}
}
