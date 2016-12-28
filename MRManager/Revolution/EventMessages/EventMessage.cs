using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EventMessage<T> : ProcessSystemMessage where T : IEntity
    {
        public T Value { get; }

        public EventMessage(T val, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Value = val;
        }
    }
}
