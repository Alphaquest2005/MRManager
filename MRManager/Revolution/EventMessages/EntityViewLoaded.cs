using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntityViewLoaded<T> : ProcessSystemMessage where T : IEntity
    {
        public IEnumerable<T> Entities { get; }
        

        public EntityViewLoaded(IEnumerable<T> entities, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            Entities = entities;
        }
    }
}
