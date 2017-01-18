using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages
{
    [Export]
    public class GetEntityViewById<TView> : ProcessSystemMessage, IGetEntityViewById<TView> where TView : IEntityView
    {
        public int EntityId { get; }

        public GetEntityViewById(int entityId, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            EntityId = entityId;
        }
    }
}