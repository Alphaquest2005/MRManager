using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{

    [Export(typeof(IEntityUpdated<>))]
    public class EntityUpdated<T> : ProcessSystemMessage, IEntityUpdated<T> where T : IEntity
    {
        public EntityUpdated() { }
        public T Entity { get; }
        
        public EntityUpdated(T entity,IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
