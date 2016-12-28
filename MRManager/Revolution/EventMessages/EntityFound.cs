using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntityFound<T> : ProcessSystemMessage where T : IEntity
    {
        public EntityFound(T entity, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Entity = entity;
        }

        public T Entity { get; }

    }
}