using System.Diagnostics.Contracts;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityUpdated<T> : BaseMessage where T : IEntity
    {
        public T Entity { get; }
        
        public EntityUpdated(T entity,MessageSource source) : base(source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
