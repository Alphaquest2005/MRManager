using System;
using System.Collections.Generic;
using System.Windows;
using SystemInterfaces;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class MainWindowViewModelInfo
    {
        public static readonly ViewModelInfo MainWindowViewModel = new ViewModelInfo
            (
            0,// set to zero to prevent ViewActorInializing this view
            new ViewInfo("MainWindowViewModel", "", ""),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {   new ViewEventSubscription<IMainWindowViewModel, IViewModelCreated<IScreenModel>>(1, e => e != null, new List<Func<IMainWindowViewModel, IViewModelCreated<IScreenModel>, bool>>
            {
                (s, e) => s.Process.Id == e.ViewModel.Process.Id 
            }, (s, e) =>
            {
                if (Application.Current == null)
                {
                    s.ScreenModel.Value = e.ViewModel;
                }
                else
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        s.ScreenModel.Value = e.ViewModel;
                    }));
                }
            })
            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IMainWindowViewModel, IViewModelLoaded<IMainWindowViewModel, IScreenModel>>(
                    key:"ScreenModelinViewModel",
                    subject: v => v.ScreenModel,
                    subjectPredicate: new List<Func<IMainWindowViewModel, bool>>
                    {
                        v => v.ScreenModel.Value != null
                    },
                    messageData: s => new ViewEventPublicationParameter(new object[] {s, s.ScreenModel.Value},new StateEventInfo(s.Process.Id, Context.ViewModel.Events.ViewModelLoaded), s.Process, s.Source ))
            }, 
            new List<IViewModelEventCommand<IViewModel, IEvent>>(),
            typeof(IMainWindowViewModel),
            typeof(IBodyViewModel)
            ,0)
            ;
    }
}