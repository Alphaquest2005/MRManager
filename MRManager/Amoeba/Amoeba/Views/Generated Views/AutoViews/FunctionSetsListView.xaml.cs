using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for FunctionSets.xaml
	/// </summary>
	public partial class FunctionSetsAutoViewList_AutoGen
	{
		public FunctionSetsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionSetsAutoViewModelDataSource") as FunctionSetsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionSetsAutoViewModel_AutoGen im;

	}
}
