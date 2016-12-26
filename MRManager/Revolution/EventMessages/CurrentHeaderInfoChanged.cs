using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CurrentHeaderInfoChanged<T> : ProcessSystemMessage where T : IEntity
    {
        public IHeaderInfo<T> Entity { get; }
    
        public CurrentHeaderInfoChanged(IHeaderInfo<T> entity, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
           Contract.Requires(entity != null);
           Entity = entity;
        }
    }
}
