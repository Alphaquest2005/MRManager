using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntitySetLoaded<T> : SystemProcessMessage where T : IEntity
    {
        public IList<T> Entities { get; }
        

        public EntitySetLoaded(IList<T> entities, ISystemProcess process, MessageSource source) : base(process, source)
        {
            Entities = entities;
        }
    }
}
