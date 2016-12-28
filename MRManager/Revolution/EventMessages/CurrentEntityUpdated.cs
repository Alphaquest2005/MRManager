using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CurrentEntityUpdated<T> : ProcessSystemMessage where T : IEntity
    {
        public T Entity { get; }
        
        public CurrentEntityUpdated(T entity, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
           Contract.Requires(entity != null || sourceMsg != null);
           
           Entity = entity;
        }
    }
}
