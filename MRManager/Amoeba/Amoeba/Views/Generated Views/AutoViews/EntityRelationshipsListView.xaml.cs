using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityRelationships.xaml
	/// </summary>
	public partial class EntityRelationshipsAutoViewList_AutoGen
	{
		public EntityRelationshipsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityRelationshipsAutoViewModelDataSource") as EntityRelationshipsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityRelationshipsAutoViewModel_AutoGen im;

	}
}
