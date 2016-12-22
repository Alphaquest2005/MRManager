using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CurrentHeaderInfoChanged<T> : SystemProcessMessage where T : IEntity
    {
        public IHeaderInfo<T> Entity { get; }
    
        public CurrentHeaderInfoChanged(IHeaderInfo<T> entity, ISystemProcess process, MessageSource source) : base(process, source)
        {
           Contract.Requires(entity != null);
           Entity = entity;
        }
    }
}
