using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class EntityUpdated<T> : ProcessSystemMessage where T : IEntity
    {
        public T Entity { get; }
        
        public EntityUpdated(T entity,ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
