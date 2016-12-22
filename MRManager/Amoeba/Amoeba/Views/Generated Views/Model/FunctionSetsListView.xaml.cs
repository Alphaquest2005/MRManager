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
	public partial class FunctionSetsList_AutoGen
	{
		public FunctionSetsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionSetsViewModelDataSource") as FunctionSetsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionSetsViewModel_AutoGen im;

	}
}
