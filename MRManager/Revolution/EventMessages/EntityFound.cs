using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntityFound<T> : ProcessSystemMessage where T : IEntity
    {
        public EntityFound(T entity, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            Entity = entity;
        }

        public T Entity { get; }

    }
}