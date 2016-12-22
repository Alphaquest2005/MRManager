using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityProperties.xaml
	/// </summary>
	public partial class EntityPropertiesAutoViewList_AutoGen
	{
		public EntityPropertiesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityPropertiesAutoViewModelDataSource") as EntityPropertiesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityPropertiesAutoViewModel_AutoGen im;

	}
}
