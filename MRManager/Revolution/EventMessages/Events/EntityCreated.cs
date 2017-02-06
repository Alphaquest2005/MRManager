using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{

    [Export(typeof(IEntityCreated<>))]
    public class EntityCreated<T> : ProcessSystemMessage, IEntityCreated<T> where T : IEntity
    {
        public T Entity { get; }
        
        public EntityCreated() { } 
        public EntityCreated(T entity, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
