using System;
using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Commands
{
    [Export(typeof(IGetEntityViewById<>))]

    public class GetEntityViewById<TView> : ProcessSystemMessage, IGetEntityViewById<TView> where TView : IEntityView
    {
        public GetEntityViewById() { }
        public int EntityId { get; }

        public GetEntityViewById(int entityId, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            EntityId = entityId;
        }
        public Type ViewType => typeof(TView);
    }
}