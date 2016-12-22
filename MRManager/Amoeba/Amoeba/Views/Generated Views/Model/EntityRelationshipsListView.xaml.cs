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
	public partial class EntityRelationshipsList_AutoGen
	{
		public EntityRelationshipsList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityRelationshipsViewModelDataSource") as EntityRelationshipsViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityRelationshipsViewModel_AutoGen im;

	}
}
