using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{


    public class CreateEntity<T> : ProcessSystemMessage, ICreateEntity<T> where T : IEntity
    {
    
        public T Entity { get; }
        
        public CreateEntity(T entity, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
         
           
        }
    }
}
