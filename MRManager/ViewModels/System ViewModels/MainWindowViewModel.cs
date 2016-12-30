﻿// <autogenerated>
//   This file was generated by T4 code generator MRManger-ViewModels.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SystemInterfaces;
using CommonMessages;
using Core.Common.UI;
using EventAggregator;
using JB.Collections.Reactive;
using RevolutionData;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewMessages;
using ViewModel.Interfaces;

namespace ViewModels
{

    public class MainWindowViewModel : BaseViewModel<MainWindowViewModel>, IMainWindowViewModel
    {
        private static readonly MainWindowViewModel instance;

        static MainWindowViewModel()
        {
            instance = new MainWindowViewModel();
        }

        public static MainWindowViewModel Instance
        {
            get { return instance; }
        }

        public MainWindowViewModel()
            : base(new SystemProcess(Processes.ProcessInfos.FirstOrDefault(),new Agent("System"), new MachineInfo(Environment.MachineName, Environment.ProcessorCount)),
                  new List<IViewModelEventSubscription<IViewModel, IEvent>>()
                  {
                      new ViewEventSubscription<MainWindowViewModel, ViewModelCreated<IScreenViewModel>>(
                        processId: 1,
                        eventPredicate: (e) => e != null,
                        actionPredicate: new List<Func<MainWindowViewModel, ViewModelCreated<IScreenViewModel>, bool>>
                        {
                            (s, e) => s.Process.Id != e.ViewModel.Process.Id
                        },
                        action: (s, e) => Application.Current.Dispatcher.Invoke(() => s.BodyViewModels.Add(e.ViewModel)))
                  },
                  new List<IViewModelEventPublication<IViewModel, IEvent>>()
                  {
                       new ViewEventPublication<MainWindowViewModel, ViewModelLoaded<IMainWindowViewModel,IScreenViewModel>>(
                        subject: v => v.BodyViewModels.CollectionChanges,
                        subjectPredicate: new List<Func<MainWindowViewModel, bool>>()
                                        {
                                            v => v.BodyViewModels.LastOrDefault() != null
                                        },
                        messageData:new List<Func<MainWindowViewModel, dynamic>>()
                                        {
                                            (s) => s,
                                            (s) => s.BodyViewModels.Last()
                                        }
                        )
                  },
                  new List<IViewModelEventCommand<IViewModel, IEvent>>(),
                  typeof(IBodyViewModel))
        {
            this.WireEvents();
        }

        //public MainWindowViewModel()
        //{
        //          EventMessageBus.Current.GetEvent<ViewModelCreated<IScreenViewModel>>(SourceMessage).Subscribe(x =>
        //          {
        //              Application.Current.Dispatcher.Invoke(() => this.DataContext = x.ViewModel);
        //              EventMessageBus.Current.Publish(new ViewLoadedViewModel<IScreenViewModel>(x.ViewModel, x.Process, SourceMessage), SourceMessage);

        //          });
        //      }


        public ObservableCollection<IViewModel> HeaderViewModels { get; } = new ObservableCollection<IViewModel>();
        public ObservableCollection<IViewModel> LeftViewModels { get; } = new ObservableCollection<IViewModel>();
        public ObservableCollection<IViewModel> RightViewModels { get; } = new ObservableCollection<IViewModel>();
        public ObservableCollection<IViewModel> FooterViewModels { get; } = new ObservableCollection<IViewModel>();
        public ObservableCollection<IViewModel> BodyViewModels { get; } = new ObservableCollection<IViewModel>();

        


    }
}
