using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EventMessage<T> : SystemProcessMessage where T : IEntity
    {
        public T Value { get; }

        public EventMessage(T val, ISystemProcess process, MessageSource source) : base(process,source)
        {
            Value = val;
        }
    }
}
