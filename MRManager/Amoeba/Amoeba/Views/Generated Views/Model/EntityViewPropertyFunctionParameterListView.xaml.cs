using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for EntityViewPropertyFunctionParameter.xaml
	/// </summary>
	public partial class EntityViewPropertyFunctionParameterList_AutoGen
	{
		public EntityViewPropertyFunctionParameterList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("EntityViewPropertyFunctionParameterViewModelDataSource") as EntityViewPropertyFunctionParameterViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		EntityViewPropertyFunctionParameterViewModel_AutoGen im;

	}
}
