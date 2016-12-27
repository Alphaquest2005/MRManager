using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class EntityUpdated<T> : ProcessSystemMessage where T : IEntity
    {
        public T Entity { get; }
        
        public EntityUpdated(T entity,ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
