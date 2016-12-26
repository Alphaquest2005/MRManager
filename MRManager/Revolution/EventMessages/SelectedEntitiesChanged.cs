using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class SelectedEntitiesChanged<T> : ProcessSystemMessage where T : IEntity
    {
        public IList<T> Changes { get; }
        
        public SelectedEntitiesChanged(IList<T> changes, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
           Changes = changes;
        }
    }
}
