using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntityFound<T> : SystemProcessMessage where T : IEntity
    {
        public EntityFound(T entity, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Entity = entity;
        }

        public T Entity { get; }

    }
}