using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    public class EntityFound<T> : ProcessSystemMessage, IEntityFound<T> where T : IEntityId
    {
        public EntityFound(T entity, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Entity = entity;
        }

        public T Entity { get; }

    }
}