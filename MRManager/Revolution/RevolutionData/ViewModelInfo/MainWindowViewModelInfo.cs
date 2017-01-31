using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SystemInterfaces;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewMessages;
using ViewModel.Interfaces;

namespace RevolutionData
{
    public class MainWindowViewModelInfo
    {
        public static readonly ViewModelInfo MainWindowViewModel = new ViewModelInfo
            (
            0,// set to zero to prevent ViewActorInializing this view
            new List<IViewModelEventSubscription<IViewModel, IEvent>>
            {   new ViewEventSubscription<IMainWindowViewModel, IViewModelCreated<IScreenModel>>(1, e => e != null, new List<Func<IMainWindowViewModel, IViewModelCreated<IScreenModel>, bool>>
            {
                (s, e) => s.Process.Id == e.ViewModel.Process.Id 
            }, (s, e) =>
            {
                if (Application.Current == null)
                {
                    s.BodyViewModels.Add(e.ViewModel);
                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        s.BodyViewModels.Add(e.ViewModel);
                    });
                }
            })
            },
            new List<IViewModelEventPublication<IViewModel, IEvent>>
            {
                new ViewEventPublication<IMainWindowViewModel, ViewModelLoaded<IMainWindowViewModel, IScreenModel>>(
                    key:"ScreenModelinViewModel",
                    subject: v => v.BodyViewModels.CollectionChanges,
                    subjectPredicate: new List<Func<IMainWindowViewModel, bool>>
                    {
                        v => v.BodyViewModels.LastOrDefault() != null
                    },
                    messageData: s => new ViewEventPublicationParameter(new object[] {s, s.BodyViewModels.Last()},new StateEventInfo(s.Process.Id, Context.ViewModel.Events.ViewModelLoaded), s.Process, s.Source ))
            }, 
            new List<IViewModelEventCommand<IViewModel, IEvent>>(),
            typeof(IMainWindowViewModel),
            typeof(IBodyViewModel));
    }
}