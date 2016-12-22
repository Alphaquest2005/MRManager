using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class StateMachinesSummaryDetailsView 
	{
		public StateMachinesSummaryDetailsView()
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
		 
		private void GoToStateMachineTriggers(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StateMachineTriggersListEXP");
			e.Handled = true;
		}
		private void GoToStateMachineTriggersAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StateMachineTriggersAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToStateMachines(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StateMachinesListEXP");
			e.Handled = true;
		}
		private void GoToStateMachinesAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("StateMachinesAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
