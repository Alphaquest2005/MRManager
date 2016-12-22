using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class LayoutSummaryDetailsView 
	{
		public LayoutSummaryDetailsView()
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
 
		private void GoToScreenLayouts(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenLayoutsListEXP");
			e.Handled = true;
		}
		private void GoToScreenLayoutsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenLayoutsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToLayout(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("LayoutListEXP");
			e.Handled = true;
		}
		private void GoToLayoutAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("LayoutAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
