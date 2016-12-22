using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CreateEntity<T> : SystemProcessMessage where T : IEntity
    {
    
        public T Entity { get; }
        
        public CreateEntity(T entity, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
         
           
        }
    }
}
