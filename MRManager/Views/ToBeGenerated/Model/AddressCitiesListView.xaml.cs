using System;
using System.Windows;


namespace Views
{
/// <summary>
	/// Interaction logic for AddressCities.xaml
	/// </summary>
	public partial class AddressCitiesListView_AutoGen
	{
		public AddressCitiesListView_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				//im = this.FindResource("AddressCitiesViewModelDataSource") as EntityViewModel;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		//EntityViewModel im;

	}
}
