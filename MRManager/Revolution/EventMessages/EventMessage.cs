using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class EventMessage<T> : ProcessSystemMessage where T : IEntity
    {
        public T Value { get; }

        public EventMessage(T val, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Value = val;
        }
    }
}
