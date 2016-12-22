using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for ModelTypes.xaml
	/// </summary>
	public partial class ModelTypesList_AutoGen
	{
		public ModelTypesList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ModelTypesViewModelDataSource") as ModelTypesViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ModelTypesViewModel_AutoGen im;

	}
}
