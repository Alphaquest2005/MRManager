using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModels;


namespace Views
{
/// <summary>
	/// Interaction logic for Views.xaml
	/// </summary>
	public partial class ViewsAutoViewList_AutoGen
	{
		public ViewsAutoViewList_AutoGen()
		{
			try
			{
				this.InitializeComponent();
				im = this.FindResource("ViewsAutoViewModelDataSource") as ViewsAutoViewModel_AutoGen;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + ex.StackTrace);
			}
		}
		ViewsAutoViewModel_AutoGen im;

	}
}
