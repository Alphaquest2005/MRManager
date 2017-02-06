using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{

    [Export(typeof(EntitySetLoaded<>))]
    public class EntitySetLoaded<TEntity> : ProcessSystemMessage, IEntitySetLoaded<TEntity> where TEntity : IEntity
    {
        public EntitySetLoaded() { }
        public IList<TEntity> Entities { get; }
        

        public EntitySetLoaded(IList<TEntity> entities, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Entities = entities;
        }
    }
}
