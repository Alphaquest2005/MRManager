using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityViewEntityProperties.xaml
	/// </summary>
	public partial class EntityViewEntityPropertiesAutoViewList_AutoGen
	{
		public EntityViewEntityPropertiesAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewEntityPropertiesAutoViewModelDataSource") as EntityViewEntityPropertiesAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewEntityPropertiesAutoViewModel_AutoGen im;

	}
}
