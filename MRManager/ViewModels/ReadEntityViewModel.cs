//using System.Collections.Generic;
//using SystemInterfaces;
//using Core.Common.UI;
//
//using ViewModel.Interfaces;

//namespace ViewModels
//{
//    public partial class ReadEntityViewModel<TEntity> : BaseReadEntityViewModel<TEntity>, IViewModel where TEntity : IEntity
//    {


//        public ReadEntityViewModel(List<IViewModelEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IViewModelEventPublication<IViewModel, IEvent>> eventPublicatons, List<IViewModelEventCommand<IViewModel, IEvent>> commandInfo, ISystemProcess process) : base(eventSubscriptions, eventPublicatons,commandInfo, process)
//        {
//            this.WireEvents();
            
//            OnTotals();
//        }

   
//        partial void OnTotals();

//    }
//}