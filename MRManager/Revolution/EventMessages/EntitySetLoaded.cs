using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{


    public class EntitySetLoaded<TEntity> : ProcessSystemMessage, IEntitySetLoaded<TEntity> where TEntity : IEntity
    {
        public IList<TEntity> Entities { get; }
        

        public EntitySetLoaded(IList<TEntity> entities, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            Entities = entities;
        }
    }
}
