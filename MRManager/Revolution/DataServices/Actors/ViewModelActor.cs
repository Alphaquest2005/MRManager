using System;
using SystemInterfaces;
using SystemMessages;
using Core.Common.UI;
using EventAggregator;
using EventMessages;
using StartUp.Messages;
using ViewMessages;
using ViewModels;

namespace DataServices.Actors
{
    public class ViewModelActor : BaseActor<ViewModelActor> 
    {
        public ViewModelActor(ISystemProcess process)
        {
            Command<LoadViewModel>(x => HandleProcessViews(x));
            //EventMessageBus.Current.GetEvent<LoadViewModel<IViewModelInfo>>(msgSource).Subscribe(HandleProcessViews);

            EventMessageBus.Current.Publish(new ServiceStarted<IViewModelService>(process, MsgSource), MsgSource);
        }

        private void HandleProcessViews(LoadViewModel pe)
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
                var vm =(TViewModel) Activator.CreateInstance(vmInfo.ViewModelInfo.ViewModelType, new object[] {vmInfo.Process, vmInfo.ViewModelInfo.Subscriptions, vmInfo.ViewModelInfo.Publications });
                vm.WireEvents();
                dynamic dvm = new DynamicViewModel<TViewModel>(vm);
                EventMessageBus.Current.Publish(new ViewModelCreated<DynamicViewModel<TViewModel>>(dvm,vmInfo.Process, MsgSource), MsgSource);
            }
            catch (Exception ex)
            {
                EventMessageBus.Current.Publish(new ProcessEventFailure(failedEventType:typeof(LoadViewModel),
                                                                        failedEventMessage: vmInfo,
                                                                        expectedEventType:typeof(ViewModelCreated<DynamicViewModel<TViewModel>>),
                                                                        exception:ex,
                                                                        msgSource:MsgSource), MsgSource);
            }
           
        }
    }

    
}