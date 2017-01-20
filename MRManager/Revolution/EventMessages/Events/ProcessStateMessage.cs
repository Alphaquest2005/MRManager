using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{

    public class UpdateProcessState<TEntity> : ProcessSystemMessage, IProcessStateMessage<TEntity> where TEntity : IEntityId
    {
        public UpdateProcessState(IProcessState<TEntity> state, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            State = state;
        }

        public IProcessState<TEntity> State { get; }
    }

    public class UpdateProcessStateList<TEntity> : ProcessSystemMessage, IProcessStateListMessage<TEntity> where TEntity : IEntityId
    {
        public UpdateProcessStateList(IProcessStateList<TEntity> state, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            State = state;
        }

        public IProcessStateList<TEntity> State { get; }
    }
}