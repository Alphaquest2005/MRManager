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
	public partial class FunctionReturnTypeAutoViewList_AutoGen
	{
		public FunctionReturnTypeAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionReturnTypeAutoViewModelDataSource") as FunctionReturnTypeAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionReturnTypeAutoViewModel_AutoGen im;

	}
}
