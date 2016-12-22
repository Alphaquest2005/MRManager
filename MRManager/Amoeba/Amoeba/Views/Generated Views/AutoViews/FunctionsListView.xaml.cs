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
	public partial class FunctionsAutoViewList_AutoGen
	{
		public FunctionsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionsAutoViewModelDataSource") as FunctionsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionsAutoViewModel_AutoGen im;

	}
}
