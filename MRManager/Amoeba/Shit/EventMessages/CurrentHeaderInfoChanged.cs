using System.Diagnostics.Contracts;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    
    public class CurrentHeaderInfoChanged<T> : BaseMessage where T : IEntity
    {
        public IHeaderInfo<T> Entity { get; }
    
        public CurrentHeaderInfoChanged(IHeaderInfo<T> entity, MessageSource source) : base(source)
        {
           Contract.Requires(entity != null);
           Entity = entity;
        }
    }
}
