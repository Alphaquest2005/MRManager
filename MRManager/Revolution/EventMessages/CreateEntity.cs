using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CreateEntity<T> : ProcessSystemMessage where T : IEntity
    {
    
        public T Entity { get; }
        
        public CreateEntity(T entity, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Contract.Requires(entity != null);
            Entity = entity;
         
           
        }
    }
}
