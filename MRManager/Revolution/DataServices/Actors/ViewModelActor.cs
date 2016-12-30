using System;
using System.Linq;
using SystemInterfaces;
using SystemMessages;
using BootStrapper;
using CommonMessages;
using Core.Common.UI;
using EventAggregator;
using EventMessages;
using StartUp.Messages;
using ViewMessages;
using ViewModel.Interfaces;
using ViewModels;

namespace DataServices.Actors
{
    public class ViewModelActor : BaseActor<ViewModelActor>, IViewModelService
    {
        public ViewModelActor(ISystemProcess process)
        {
            Command<LoadViewModel>(x => HandleProcessViews(x));
            //EventMessageBus.Current.GetEvent<LoadViewModel<IViewModelInfo>>(SourceMessage).Subscribe(HandleProcessViews);

            EventMessageBus.Current.Publish(new ServiceStarted<IViewModelService>(this,process, SourceMessage), SourceMessage);
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
                EventMessageBus.Current.Publish(new ViewModelCreated<TViewModel>(vm, vmInfo.Process, SourceMessage), SourceMessage);
                EventMessageBus.Current.Publish(new ViewModelCreated<IViewModel>(vm, vmInfo.Process, SourceMessage), SourceMessage);
                //dynamic dvm = new DynamicViewModel<TViewModel>(vm);
                // EventMessageBus.Current.Publish(new ViewModelCreated<DynamicViewModel<TViewModel>>(dvm,vmInfo.Process, SourceMessage), SourceMessage);
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType:typeof(ILoadViewModel),
                                                                        failedEventMessage: vmInfo,
                                                                        expectedEventType:typeof(IViewModelCreated<IDynamicViewModel<TViewModel>>),
                                                                        exception:ex,
                                                                        SourceMsg:SourceMessage), SourceMessage);
            }
           
        }
    }

    
}