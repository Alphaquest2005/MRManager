using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntitySetLoaded<T> : ProcessSystemMessage where T : IEntity
    {
        public IList<T> Entities { get; }
        

        public EntitySetLoaded(IList<T> entities, ISystemProcess process, ISourceMessage msg) : base(process, msg)
        {
            Entities = entities;
        }
    }
}
