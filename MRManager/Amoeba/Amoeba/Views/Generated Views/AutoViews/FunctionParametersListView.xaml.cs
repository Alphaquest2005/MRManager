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
	public partial class FunctionParametersAutoViewList_AutoGen
	{
		public FunctionParametersAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionParametersAutoViewModelDataSource") as FunctionParametersAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionParametersAutoViewModel_AutoGen im;

	}
}
