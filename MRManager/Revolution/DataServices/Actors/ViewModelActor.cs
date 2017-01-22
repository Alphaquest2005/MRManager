﻿using System;
using System.Linq;
using SystemInterfaces;
using SystemMessages;
using Akka.Actor;
using BootStrapper;
using CommonMessages;
using EventAggregator;
using EventMessages;
using RevolutionEntities.Process;
using StartUp.Messages;
using ViewMessages;
using ViewModel.Interfaces;

namespace DataServices.Actors
{
    public class ViewModelActor : BaseActor<ViewModelActor>, IViewModelService
    {
        private IUntypedActorContext ctx = null;
        public ViewModelActor(ISystemProcess process)
        {
            ctx = Context;
            Command<LoadViewModel>(x => HandleProcessViews(x));
            //EventMessageBus.Current.GetEvent<LoadViewModel<IViewModelInfo>>(source).Subscribe(HandleProcessViews);

            EventMessageBus.Current.Publish(new ServiceStarted<IViewModelService>(this,new StateEventInfo(process.Id, RevolutionData.Context.Actor.Events.ActorStarted), process, Source), Source);
        }

        private void HandleProcessViews(ILoadViewModel pe)
        {
            this.GetType()
                    .GetMethod("LoadEntityViewModel")
                    .MakeGenericMethod(pe.ViewModelInfo.ViewModelType)
                    .Invoke(this, new object[] { pe });
            //LoadEntityViewModel(pe.ViewModelInfo.ViewModelType, pe.ViewModelInfo);
          
        }

        public void LoadEntityViewModel<TViewModel>(LoadViewModel vmInfo) where TViewModel:IViewModel 
        {
            try
            {
                var concreteVM = BootStrapper.BootStrapper.Container.GetExportedTypes<TViewModel>().FirstOrDefault();
                var vm =(TViewModel) Activator.CreateInstance( concreteVM/*vmInfo.ViewModelInfo.ViewModelType*/, new object[] {vmInfo.Process, vmInfo.ViewModelInfo.Subscriptions, vmInfo.ViewModelInfo.Publications, vmInfo.ViewModelInfo.Commands, vmInfo.ViewModelInfo.Orientation });
                EventMessageBus.Current.Publish(new ViewModelCreated<TViewModel>(vm, new StateEventInfo(vmInfo.Process.Id, RevolutionData.Context.ViewModel.Events.ViewModelCreated), vmInfo.Process, Source), Source);
                EventMessageBus.Current.Publish(new ViewModelCreated<IViewModel>(vm, new StateEventInfo(vmInfo.Process.Id, RevolutionData.Context.ViewModel.Events.ViewModelCreated), vmInfo.Process, Source), Source);
                //dynamic dvm = new DynamicViewModel<TViewModel>(vm);
                // EventMessageBus.Current.Publish(new ViewModelCreated<DynamicViewModel<TViewModel>>(dvm,vmInfo.Process, source), source);
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ILoadViewModel),
                                                                        failedEventMessage: vmInfo,
                                                                        expectedEventType: typeof(IViewModelCreated<IDynamicViewModel<TViewModel>>),
                                                                        exception: ex,
                                                                        source: Source, processInfo: new StateEventInfo(vmInfo.Process.Id, RevolutionData.Context.Process.Events.Error)), Source);
            }
           
        }
    }

    
}