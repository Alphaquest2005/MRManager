using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class EventMessage<T> : ProcessSystemMessage where T : IEntity
    {
        public T Value { get; }

        public EventMessage(T val, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Value = val;
        }
    }
}
