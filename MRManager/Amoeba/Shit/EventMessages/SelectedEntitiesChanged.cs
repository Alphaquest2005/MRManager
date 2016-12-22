using System.Collections.Generic;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class SelectedEntitiesChanged<T> : BaseMessage where T : IEntity
    {
        public IList<T> Changes { get; }
        
        public SelectedEntitiesChanged(IList<T> changes, MessageSource source) : base(source)
        {
           Changes = changes;
        }
    }
}
