using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CurrentEntityUpdated<T> : ProcessSystemMessage where T : IEntity
    {
        public T Entity { get; }
        
        public CurrentEntityUpdated(T entity, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
           Contract.Requires(entity != null || msg != null);
           
           Entity = entity;
        }
    }
}
