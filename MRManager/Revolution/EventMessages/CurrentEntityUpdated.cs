using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    
    public class CurrentEntityUpdated<T> : ProcessSystemMessage where T : IEntity
    {
        public T Entity { get; }
        
        public CurrentEntityUpdated(T entity, ISystemProcess process, ISystemSource source) : base(process, source)
        {
           Contract.Requires(entity != null || source != null);
           
           Entity = entity;
        }
    }
}
