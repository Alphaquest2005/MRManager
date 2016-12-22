using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ApplicationEntities.xaml
	/// </summary>
	public partial class ApplicationEntitiesList_AutoGen
	{
		public ApplicationEntitiesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ApplicationEntitiesViewModelDataSource") as ApplicationEntitiesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ApplicationEntitiesViewModel_AutoGen im;

	}
}
