using System.Collections.Generic;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;

namespace EventMessages
{
    public class EntityViewLoaded<T> : SystemProcessMessage where T : IEntity
    {
        public IEnumerable<T> Entities { get; }
        

        public EntityViewLoaded(IEnumerable<T> entities, ISystemProcess process, MessageSource source) : base(process,source)
        {
            Entities = entities;
        }
    }
}
