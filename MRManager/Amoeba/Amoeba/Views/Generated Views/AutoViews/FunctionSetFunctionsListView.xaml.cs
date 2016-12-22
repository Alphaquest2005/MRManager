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
	public partial class FunctionSetFunctionsAutoViewList_AutoGen
	{
		public FunctionSetFunctionsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionSetFunctionsAutoViewModelDataSource") as FunctionSetFunctionsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionSetFunctionsAutoViewModel_AutoGen im;

	}
}
