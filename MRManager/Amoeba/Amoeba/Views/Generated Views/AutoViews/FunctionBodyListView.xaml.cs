using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for FunctionBody.xaml
	/// </summary>
	public partial class FunctionBodyAutoViewList_AutoGen
	{
		public FunctionBodyAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionBodyAutoViewModelDataSource") as FunctionBodyAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionBodyAutoViewModel_AutoGen im;

	}
}
