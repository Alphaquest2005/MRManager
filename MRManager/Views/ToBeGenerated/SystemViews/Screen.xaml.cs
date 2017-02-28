using Core.Common.UI;
using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SystemInterfaces;
using Common;
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
        //public ISystemSource Source => new Source(Guid.NewGuid(), $"View:{typeof(Screen)}>",new SourceType(typeof(Screen)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        public Screen()
        {

            try
            {
                // Required to initialize variables
                //InitializeComponent();
                //AppSlider.Slider = this.slider;
                ////TODO: Figure out the proper way to bind and get this out xaml is not a source

                //EventMessageBus.Current.GetEvent<IViewModelCreated<IScreenModel>>(Source).Subscribe(x =>
                //{
                //    Application.Current.Dispatcher.Invoke(() =>
                //    {
                //        this.DataContext = x.ViewModel;
                //        x.ViewModel.Slider = this.slider;
                //    });
                //});




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
