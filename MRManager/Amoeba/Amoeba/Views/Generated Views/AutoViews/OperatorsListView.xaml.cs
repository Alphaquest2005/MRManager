using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Operators.xaml
	/// </summary>
	public partial class OperatorsAutoViewList_AutoGen
	{
		public OperatorsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("OperatorsAutoViewModelDataSource") as OperatorsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		OperatorsAutoViewModel_AutoGen im;

	}
}
