using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
  

    public class EntitySetWithFilterLoaded<TEntity> : ProcessSystemMessage, IEntitySetWithFilterLoaded<TEntity> where TEntity : IEntity
    {
        public IList<TEntity> Entities { get; }
        

        public EntitySetWithFilterLoaded(IList<TEntity> entities, IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            Entities = entities;
        }
    }
}
