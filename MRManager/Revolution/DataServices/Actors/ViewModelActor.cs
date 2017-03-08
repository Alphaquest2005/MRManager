using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using SystemInterfaces;
using Akka.Actor;
using BootStrapper;
using CommonMessages;
using EventAggregator;
using EventMessages;
using EventMessages.Events;
using RevolutionData;
using RevolutionEntities.Process;
using StartUp.Messages;
using ViewMessages;
using ViewModel.Interfaces;

namespace DataServices.Actors
{
    public class ViewModelActor : BaseActor<ViewModelActor>, IViewModelService
    {
        private IUntypedActorContext ctx = null;
        public ViewModelActor(ISystemProcess process):base(process)
        {
            ctx = Context;
            Command<LoadViewModel>(x => HandleProcessViews(x));

            EventMessageBus.Current.GetEvent<ILoadViewModel>(Source).Subscribe(x => HandleProcessViews(x));
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
                var concreteVM = BootStrapper.BootStrapper.Container.GetExportedTypes<TViewModel>().FirstOrDefault() ?? BootStrapper.BootStrapper.Container.GetExportedType(vmInfo.ViewModelInfo.ViewModelType);

                var vm =(TViewModel) Activator.CreateInstance( concreteVM, new object[] {vmInfo.Process,vmInfo.ViewModelInfo.ViewInfo, vmInfo.ViewModelInfo.Subscriptions, vmInfo.ViewModelInfo.Publications, vmInfo.ViewModelInfo.Commands, vmInfo.ViewModelInfo.Orientation, vmInfo.ViewModelInfo.Priority });
                EventMessageBus.Current.Publish(new ViewModelCreated<TViewModel>(vm, new StateEventInfo(vmInfo.Process.Id, RevolutionData.Context.ViewModel.Events.ViewModelCreated), vmInfo.Process, Source), Source);
                EventMessageBus.Current.Publish(new ViewModelCreated<IViewModel>(vm, new StateEventInfo(vmInfo.Process.Id, RevolutionData.Context.ViewModel.Events.ViewModelCreated), vmInfo.Process, Source), Source);
                //dynamic dvm = new DynamicViewModel<TViewModel>(vm);
                // EventMessageBus.Current.Publish(new ViewModelCreated<DynamicViewModel<TViewModel>>(dvm,vmInfo.Process, source), source);
            }
            catch (Exception ex)
            {
                Debugger.Break();
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType: typeof(ILoadViewModel),
                                                                        failedEventMessage: vmInfo,
                                                                        expectedEventType: typeof(IViewModelCreated<IDynamicViewModel<TViewModel>>),
                                                                        exception: ex,
                                                                        source: Source, processInfo: new StateEventInfo(vmInfo.Process.Id, RevolutionData.Context.Process.Events.Error)), Source);
            }
           
        }
    }

    
}