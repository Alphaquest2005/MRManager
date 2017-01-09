using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class FilterChanged<T> : ProcessSystemMessage where T : IEntity
    {
        public string Filter { get; }
        
        public FilterChanged(string filter, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Filter = filter;
        }

    }
}
