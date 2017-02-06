using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{

    [Export(typeof(IEntitySetWithFilterLoaded<>))]
    public class EntitySetWithFilterLoaded<TEntity> : ProcessSystemMessage, IEntitySetWithFilterLoaded<TEntity> where TEntity : IEntity
    {
        public EntitySetWithFilterLoaded() { }
        public IList<TEntity> Entities { get; }
        

        public EntitySetWithFilterLoaded(IList<TEntity> entities, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Entities = entities;
        }
    }
}
