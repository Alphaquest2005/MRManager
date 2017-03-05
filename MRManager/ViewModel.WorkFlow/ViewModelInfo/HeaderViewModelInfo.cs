using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using SystemInterfaces;
using ReactiveUI;
using RevolutionEntities.Process;
using RevolutionEntities.ViewModels;
using ViewMessages;
using ViewModel.Interfaces;

namespace RevolutionData
{
}

namespace RevolutionData
{
    public class HeaderViewModelInfo
    {
        public static readonly ViewModelInfo HeaderViewModel = new ViewModelInfo
            (
            3,
            new ViewInfo("HeaderViewModel", "", ""),
            new List<IViewModelEventSubscription<IViewModel, IEvent>>{},
            new List<IViewModelEventPublication<IViewModel, IEvent>>{},
            new List<IViewModelEventCommand<IViewModel,IEvent>>
            {


                new ViewEventCommand<IHeaderViewModel, INavigateToView>(
                    key:"ViewHome",
                    commandPredicate:new List<Func<IHeaderViewModel, bool>>{},
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s =>
                    {
                        return new ViewEventCommandParameter(
                            new object[] {ViewMessageConst.Instance.ViewHome},
                            new StateCommandInfo(s.Process.Id,
                                Context.View.Commands.NavigateToView), s.Process,
                            s.Source);
                    }),

                new ViewEventCommand<IHeaderViewModel, INavigateToView>(
                    key:"ViewPatientInfo",
                    commandPredicate:new List<Func<IHeaderViewModel, bool>>{},
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s =>
                    {
                        return new ViewEventCommandParameter(
                            new object[] {ViewMessageConst.Instance.ViewPatientSummary},
                            new StateCommandInfo(s.Process.Id,
                                Context.View.Commands.NavigateToView), s.Process,
                            s.Source);
                    }),
                

                new ViewEventCommand<IHeaderViewModel, INavigateToView>(
                    key:"ViewPatientResponses",
                    commandPredicate:new List<Func<IHeaderViewModel, bool>>{},
                    subject:s => Observable.Empty<ReactiveCommand<IViewModel, Unit>>(),

                    messageData: s =>
                    {
                        return new ViewEventCommandParameter(
                            new object[] {ViewMessageConst.Instance.ViewPatientResponses},
                            new StateCommandInfo(s.Process.Id,
                                Context.View.Commands.NavigateToView), s.Process,
                            s.Source);
                    }),
                   



            },
            typeof(IHeaderViewModel),
            typeof(IHeaderViewModel), 0);
    }
}