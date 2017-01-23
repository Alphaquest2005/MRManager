using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export]
    public class GetEntityById<T> : ProcessSystemMessage, IGetEntityById<T> where T : IEntity
    {
        public int EntityId { get; }

        public GetEntityById( int entityId , IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            EntityId = entityId;
        }
    }
}
