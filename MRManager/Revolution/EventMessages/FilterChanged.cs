using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class FilterChanged<T> : SystemProcessMessage where T : IEntity
    {
        public string Filter { get; }
        
        public FilterChanged(string filter, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Filter = filter;
        }

    }
}
