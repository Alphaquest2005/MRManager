using System.Diagnostics.Contracts;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{


    public class EntityUpdated<T> : ProcessSystemMessage, IEntityUpdated<T> where T : IEntity
    {
        public T Entity { get; }
        
        public EntityUpdated(T entity,IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Contract.Requires(entity != null);
            Entity = entity;
        }

    }
}
