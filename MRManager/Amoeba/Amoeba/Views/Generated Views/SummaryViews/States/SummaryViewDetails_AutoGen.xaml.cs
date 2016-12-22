using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class StatesSummaryDetailsView 
	{
		public StatesSummaryDetailsView()
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
 
		private void GoToStateMachineStates(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StateMachineStatesListEXP");
			e.Handled = true;
		}
		private void GoToStateMachineStatesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StateMachineStatesAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToStates(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StatesListEXP");
			e.Handled = true;
		}
		private void GoToStatesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StatesAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
