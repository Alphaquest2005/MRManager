using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class FilterChanged<T> : BaseMessage where T : IEntity
    {
        public string Filter { get; }
        
        public FilterChanged(string filter, MessageSource source) : base(source)
        {
            Filter = filter;
        }

    }
}
