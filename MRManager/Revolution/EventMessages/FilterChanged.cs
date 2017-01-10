using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class FilterChanged<T> : ProcessSystemMessage where T : IEntity
    {
        public string Filter { get; }
        
        public FilterChanged(string filter, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Filter = filter;
        }

    }
}
