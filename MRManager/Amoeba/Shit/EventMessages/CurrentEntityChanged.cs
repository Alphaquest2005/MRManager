using System.Diagnostics.Contracts;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CurrentEntityChanged<T> : BaseMessage where T : IEntity
    {
        public T Entity { get; }
        
        public CurrentEntityChanged(T entity, MessageSource source) : base(source)
        {
           Contract.Requires(entity != null || source != null);
           
           Entity = entity;
        }
    }
}
