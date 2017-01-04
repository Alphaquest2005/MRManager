
using Core.Common.UI;
using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommonMessages;
using EventAggregator;
using RevolutionEntities.Process;
using ViewMessages;
using ViewModel.Interfaces;
using ViewModels;

namespace Views
{
	public partial class Screen
	{
        protected SourceMessage SourceMessage => new SourceMessage(new MessageSource(this.ToString()), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
		public Screen()
		{
            
			try
			{
                // Required to initialize variables
                InitializeComponent();
				AppSlider.Slider = this.slider;
			   
			}
			catch (Exception)
			{
				throw;
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
