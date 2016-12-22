
using Core.Common.UI;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views
{
	public partial class MainView_AutoGen 
	{
		public MainView_AutoGen()
		{
			try
			{
				// Required to initialize variables
				InitializeComponent();
				AppSlider.Slider = this.slider;
				//AppSlider.Slider.MoveTo("SummaryEXP");
			}
			catch (Exception)
			{
				throw;
			}
		}

		private void BackBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (IsMouseOver == true)
			{
				AppSlider.Slider.MoveToPreviousCtl();
			}
		}

		private void BringIntoView(object sender, MouseEventArgs e)
		{
			BringIntoView(sender);
		}

		private static void BringIntoView(object sender)
		{
			if (typeof(Expander).IsInstanceOfType(sender))
			{
				AppSlider.Slider.BringIntoView(((FrameworkElement)sender) as Expander);
			}
			else
			{
				Expander p = ((FrameworkElement)sender).Parent as Expander;
				//  p.IsExpanded = true;
				p.UpdateLayout();
				AppSlider.Slider.BringIntoView(p);
			}
		}
	}
}
