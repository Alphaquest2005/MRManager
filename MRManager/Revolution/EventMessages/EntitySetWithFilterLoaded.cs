using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
  

    public class EntitySetWithFilterLoaded<TEntity> : ProcessSystemMessage, IEntitySetWithFilterLoaded<TEntity> where TEntity : IEntity
    {
        public IList<TEntity> Entities { get; }
        

        public EntitySetWithFilterLoaded(IList<TEntity> entities, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Entities = entities;
        }
    }
}
