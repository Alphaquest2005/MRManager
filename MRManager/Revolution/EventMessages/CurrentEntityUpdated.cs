using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CurrentEntityUpdated<T> : SystemProcessMessage where T : IEntity
    {
        public T Entity { get; }
        
        public CurrentEntityUpdated(T entity, ISystemProcess process, MessageSource source) : base(process,source)
        {
           Contract.Requires(entity != null || source != null);
           
           Entity = entity;
        }
    }
}
