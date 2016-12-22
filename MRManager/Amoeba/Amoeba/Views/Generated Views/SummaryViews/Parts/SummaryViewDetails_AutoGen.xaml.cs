using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class PartsSummaryDetailsView 
	{
		public PartsSummaryDetailsView()
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
 
		private void GoToScreenParts(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenPartsListEXP");
			e.Handled = true;
		}
		private void GoToScreenPartsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("ScreenPartsAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToParts(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PartsListEXP");
			e.Handled = true;
		}
		private void GoToPartsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("PartsAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
