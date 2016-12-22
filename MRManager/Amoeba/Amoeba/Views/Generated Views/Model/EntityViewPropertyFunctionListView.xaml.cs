using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityViewPropertyFunction.xaml
	/// </summary>
	public partial class EntityViewPropertyFunctionList_AutoGen
	{
		public EntityViewPropertyFunctionList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewPropertyFunctionViewModelDataSource") as EntityViewPropertyFunctionViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewPropertyFunctionViewModel_AutoGen im;

	}
}
