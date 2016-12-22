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
	public partial class ApplicationEntitiesAutoViewList_AutoGen
	{
		public ApplicationEntitiesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ApplicationEntitiesAutoViewModelDataSource") as ApplicationEntitiesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ApplicationEntitiesAutoViewModel_AutoGen im;

	}
}
