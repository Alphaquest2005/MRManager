using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityView.xaml
	/// </summary>
	public partial class EntityViewList_AutoGen
	{
		public EntityViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewViewModelDataSource") as EntityViewViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewViewModel_AutoGen im;

	}
}
