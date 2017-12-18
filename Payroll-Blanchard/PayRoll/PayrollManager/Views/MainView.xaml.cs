using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PayrollManager
{
	public partial class MainView : UserControl
	{
		public MainView()
		{
			// Required to initialize variables
			InitializeComponent();
            BaseViewModel.slider = this.slider;
            BaseViewModel.slider.MoveTo("IntroEXP");
		}

        private void BackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseOver == true)
            {
                BaseViewModel.slider.MoveToPreviousCtl();
            }
        }

        private void GotoEmployeeDetails(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("EmployeeDetailsEXP");
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            BaseViewModel.slider.MoveTo("PayrollEmployeeSetupDetailsBDR");
        }

        private void PayrollItemDetailsEXP_Expanded(object sender, RoutedEventArgs e)
        {
            BaseViewModel.slider.BringIntoView("PayrollItemDetailsEXP");
        }


        private void BringIntoView(object sender, MouseEventArgs e)
        {
            BringIntoView(sender);
        }

        private static void BringIntoView(object sender)
        {
            if (typeof(Expander).IsInstanceOfType(sender))
            {
                BaseViewModel.slider.BringIntoView(((FrameworkElement)sender) as Expander);
            }
            else
            {
                //Expander p = ((FrameworkElement)sender).Parent as Expander;
                ////  p.IsExpanded = true;
                //p.UpdateLayout();
                //BaseViewModel.slider.BringIntoView(p);
            }
        }

       

     
	}
}