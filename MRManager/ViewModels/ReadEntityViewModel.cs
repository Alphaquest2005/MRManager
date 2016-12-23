using System.Collections.Generic;
using SystemInterfaces;
using Core.Common.UI;
using DataInterfaces;

namespace ViewModels
{
    public partial class ReadEntityViewModel<TEntity> : BaseReadEntityViewModel<TEntity>, IViewModel where TEntity : IEntity
    {


        public ReadEntityViewModel(List<IEventSubscription<IViewModel, IEvent>> eventSubscriptions, List<IEventPublication<IViewModel, IEvent>> eventPublicatons, List<IViewCommand<IViewModel, IEvent>> commands, ISystemProcess process) : base(eventSubscriptions, eventPublicatons,commands, process)
        {
            this.WireEvents();
            
            OnTotals();
        }

   
        partial void OnTotals();

    }
}