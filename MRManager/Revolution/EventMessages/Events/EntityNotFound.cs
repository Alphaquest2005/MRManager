using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{


    [Export(typeof(IEntityNotFound))]
    public class EntityNotFound<T> : ProcessSystemMessage, IEntityNotFound where T : IEntity
    {
        public EntityNotFound() { }
        public EntityNotFound(int entityId, IStateEventInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            EntityId = entityId;
        }

        public int EntityId { get; }

    }
}
