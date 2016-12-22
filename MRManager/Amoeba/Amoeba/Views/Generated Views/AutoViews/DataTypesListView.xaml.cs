using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for DataTypes.xaml
	/// </summary>
	public partial class DataTypesAutoViewList_AutoGen
	{
		public DataTypesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("DataTypesAutoViewModelDataSource") as DataTypesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		DataTypesAutoViewModel_AutoGen im;

	}
}
