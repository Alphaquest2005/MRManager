using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class FilterChanged<T> : ProcessSystemMessage where T : IEntity
    {
        public string Filter { get; }
        
        public FilterChanged(string filter, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            Filter = filter;
        }

    }
}
