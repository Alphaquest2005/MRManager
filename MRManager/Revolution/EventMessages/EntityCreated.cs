using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityCreated<T> : SystemProcessMessage where T : IEntity
    {
        public T Entity { get; }
        
        public EntityCreated(T entity, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
