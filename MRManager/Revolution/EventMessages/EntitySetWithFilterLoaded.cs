using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntitySetWithFilterLoaded<T> : ProcessSystemMessage where T : IEntity
    {
        public IList<T> Entities { get; }
        

        public EntitySetWithFilterLoaded(IList<T> entities, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Entities = entities;
        }
    }
}
