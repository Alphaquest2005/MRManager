using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for TestValues.xaml
	/// </summary>
	public partial class TestValuesAutoViewList_AutoGen
	{
		public TestValuesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("TestValuesAutoViewModelDataSource") as TestValuesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		TestValuesAutoViewModel_AutoGen im;

	}
}
