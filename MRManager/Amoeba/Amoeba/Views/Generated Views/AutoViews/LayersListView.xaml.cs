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
	public partial class LayersAutoViewList_AutoGen
	{
		public LayersAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("LayersAutoViewModelDataSource") as LayersAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		LayersAutoViewModel_AutoGen im;

	}
}
