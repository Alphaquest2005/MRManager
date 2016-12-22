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
	public partial class DataTypesList_AutoGen
	{
		public DataTypesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("DataTypesViewModelDataSource") as DataTypesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		DataTypesViewModel_AutoGen im;

	}
}
