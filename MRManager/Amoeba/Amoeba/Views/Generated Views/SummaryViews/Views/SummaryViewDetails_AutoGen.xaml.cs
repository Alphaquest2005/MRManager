using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class ViewsSummaryDetailsView 
	{
		public ViewsSummaryDetailsView()
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
 
		private void GoToScreenViews(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenViewsListEXP");
			e.Handled = true;
		}
		private void GoToScreenViewsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenViewsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToViewEntity(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ViewEntityListEXP");
			e.Handled = true;
		}
		private void GoToViewEntityAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ViewEntityAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToViews(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ViewsListEXP");
			e.Handled = true;
		}
		private void GoToViewsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ViewsAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
