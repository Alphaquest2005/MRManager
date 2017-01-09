using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{


    public class EntityUpdated<T> : ProcessSystemMessage, IEntityUpdated<T> where T : IEntity
    {
        public T Entity { get; }
        
        public EntityUpdated(T entity,ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
