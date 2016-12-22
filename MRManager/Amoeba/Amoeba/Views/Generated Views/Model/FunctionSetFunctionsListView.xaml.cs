using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for FunctionSetFunctions.xaml
	/// </summary>
	public partial class FunctionSetFunctionsList_AutoGen
	{
		public FunctionSetFunctionsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionSetFunctionsViewModelDataSource") as FunctionSetFunctionsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionSetFunctionsViewModel_AutoGen im;

	}
}
