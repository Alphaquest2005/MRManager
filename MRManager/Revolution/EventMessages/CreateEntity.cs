using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{


    public class CreateEntity<T> : ProcessSystemMessage, ICreateEntity<T> where T : IEntity
    {
    
        public T Entity { get; }
        
        public CreateEntity(T entity, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
         
           
        }
    }
}
