using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntityViewLoaded<T> : ProcessSystemMessage where T : IEntity
    {
        public IEnumerable<T> Entities { get; }
        

        public EntityViewLoaded(IEnumerable<T> entities, ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
            Entities = entities;
        }
    }
}
