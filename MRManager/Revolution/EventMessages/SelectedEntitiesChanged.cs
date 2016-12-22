using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class SelectedEntitiesChanged<T> : SystemProcessMessage where T : IEntity
    {
        public IList<T> Changes { get; }
        
        public SelectedEntitiesChanged(IList<T> changes, ISystemProcess process, MessageSource source) : base(process, source)
        {
           Changes = changes;
        }
    }
}
