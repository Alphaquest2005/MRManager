using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{
    [Export]
    public class GetEntityById<T> : ProcessSystemMessage, IGetEntityById<T> where T : IEntity
    {
        public int EntityId { get; }

        public GetEntityById( int entityId ,ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            EntityId = entityId;
        }
    }

    [Export]
    public class GetEntityViewById<TView> : ProcessSystemMessage, IGetEntityViewById<TView>
    {
        public int EntityId { get; }

        public GetEntityViewById(int entityId, ISystemProcess process, ISourceMessage sourceMsg) : base(process, sourceMsg)
        {
            EntityId = entityId;
        }
    }
}
