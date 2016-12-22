using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for FunctionReturnType.xaml
	/// </summary>
	public partial class FunctionReturnTypeList_AutoGen
	{
		public FunctionReturnTypeList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionReturnTypeViewModelDataSource") as FunctionReturnTypeViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionReturnTypeViewModel_AutoGen im;

	}
}
