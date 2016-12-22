using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EventMessage<T> : BaseMessage where T : IEntity
    {
        public T Value { get; }

        public EventMessage(T val, MessageSource source) : base(source)
        {
            Value = val;
        }
    }
}
