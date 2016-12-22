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
	public partial class FunctionBodyList_AutoGen
	{
		public FunctionBodyList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("FunctionBodyViewModelDataSource") as FunctionBodyViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		FunctionBodyViewModel_AutoGen im;

	}
}
