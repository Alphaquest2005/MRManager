using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CreateEntity<T> : ProcessSystemMessage where T : IEntity
    {
    
        public T Entity { get; }
        
        public CreateEntity(T entity, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            Contract.Requires(entity != null);
            Entity = entity;
         
           
        }
    }
}
