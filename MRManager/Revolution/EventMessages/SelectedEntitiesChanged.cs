using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class SelectedEntitiesChanged<T> : ProcessSystemMessage where T : IEntity
    {
        public IList<T> Changes { get; }
        
        public SelectedEntitiesChanged(IList<T> changes, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
           Changes = changes;
        }
    }
}
