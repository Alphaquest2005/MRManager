
using Core.Common.UI;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommonMessages;
using EventAggregator;
using ViewMessages;
using ViewModels;

namespace Views
{
	public partial class Screen
	{
        protected MessageSource MsgSource => new MessageSource(this.ToString());
		public Screen()
		{
            
			try
			{
                // Required to initialize variables
                InitializeComponent();
				AppSlider.Slider = this.slider;
			    EventMessageBus.Current.GetEvent<ViewModelCreated<DynamicViewModel<ScreenModel>>>(MsgSource).Subscribe(x =>
			    {
			        Application.Current.Dispatcher.Invoke(() => this.DataContext = x.ViewModel);
                    EventMessageBus.Current.Publish(new ViewLoadedViewModel<DynamicViewModel<ScreenModel>>(x.ViewModel, x.Process, x), MsgSource );

			    });
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
