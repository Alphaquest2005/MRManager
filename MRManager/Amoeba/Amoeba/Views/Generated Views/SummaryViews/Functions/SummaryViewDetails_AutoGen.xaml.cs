using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	/// <summary>
	/// Interaction logic for SummaryList.xaml
	/// </summary>
	public partial class FunctionsSummaryDetailsView 
	{
		public FunctionsSummaryDetailsView()
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
 
		private void GoToEntityViewPropertyFunctionParameter(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertyFunctionParameterListEXP");
			e.Handled = true;
		}
		private void GoToEntityViewPropertyFunctionParameterAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertyFunctionParameterAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToEntityViewPropertyFunction(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertyFunctionListEXP");
			e.Handled = true;
		}
		private void GoToEntityViewPropertyFunctionAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("EntityViewPropertyFunctionAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToFunctionBody(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionBodyListEXP");
			e.Handled = true;
		}
		private void GoToFunctionBodyAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionBodyAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToFunctionParameters(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionParametersListEXP");
			e.Handled = true;
		}
		private void GoToFunctionParametersAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionParametersAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToFunctionReturnType(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionReturnTypeListEXP");
			e.Handled = true;
		}
		private void GoToFunctionReturnTypeAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionReturnTypeAutoViewListEXP");
			e.Handled = true;
		}
		 
		private void GoToFunctions(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionsListEXP");
			e.Handled = true;
		}
		private void GoToFunctionsAutoView(object sender, MouseButtonEventArgs e)
		{
			Slider.BringIntoView("FunctionsAutoViewListEXP");
			e.Handled = true;
		}
		  
	}
}
