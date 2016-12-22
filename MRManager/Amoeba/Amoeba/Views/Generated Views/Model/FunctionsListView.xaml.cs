using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Functions.xaml
	/// </summary>
	public partial class FunctionsList_AutoGen
	{
		public FunctionsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionsViewModelDataSource") as FunctionsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionsViewModel_AutoGen im;

	}
}
