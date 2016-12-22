using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for RelationshipTypes.xaml
	/// </summary>
	public partial class RelationshipTypesAutoViewList_AutoGen
	{
		public RelationshipTypesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("RelationshipTypesAutoViewModelDataSource") as RelationshipTypesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		RelationshipTypesAutoViewModel_AutoGen im;

	}
}
