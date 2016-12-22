using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class ModelTypesSummaryDetailsView 
	{
		public ModelTypesSummaryDetailsView()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
		}

		private void BringIntoView(object sender, MouseEventArgs e)
		{
			BringIntoView(sender);
			e.Handled = true;
		}

		private void BringIntoView(object sender)
		{
			if (typeof (Expander).IsInstanceOfType(sender))
			{
				Slider.BringIntoView(((FrameworkElement) sender) as Expander);
			}
			else
			{
				Expander p = ((FrameworkElement) sender).Parent as Expander;
				p.UpdateLayout();
				Slider.BringIntoView(p);
			}
		}
 
		private void GoToPrimaryKeyOptions(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PrimaryKeyOptionsListEXP");
			e.Handled = true;
		}
		private void GoToPrimaryKeyOptionsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PrimaryKeyOptionsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToDataProperties(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("DataPropertiesListEXP");
			e.Handled = true;
		}
		private void GoToDataPropertiesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("DataPropertiesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToModelTypes(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ModelTypesListEXP");
			e.Handled = true;
		}
		private void GoToModelTypesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ModelTypesAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
