using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Layers.xaml
	/// </summary>
	public partial class LayersList_AutoGen
	{
		public LayersList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("LayersViewModelDataSource") as LayersViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		LayersViewModel_AutoGen im;

	}
}
