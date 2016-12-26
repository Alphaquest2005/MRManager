using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityCreated<T> : ProcessSystemMessage where T : IEntity
    {
        public T Entity { get; }
        
        public EntityCreated(T entity, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
