using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IEntityFound<>))]
    public class EntityFound<T> : ProcessSystemMessage, IEntityFound<T> where T : IEntityId
    {
        public EntityFound() { }
        public EntityFound(T entity, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Entity = entity;
        }

        public T Entity { get; }

    }
}