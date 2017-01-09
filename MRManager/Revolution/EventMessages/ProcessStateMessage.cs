using SystemInterfaces;
using CommonMessages;


namespace EventMessages
{

    public class ProcessStateMessage<TEntity> : ProcessSystemMessage, IProcessStateMessage<TEntity> where TEntity : IEntityId
    {
        public ProcessStateMessage(IProcessState<TEntity> state, ISystemProcess process, ISystemSource source) : base(process, source)
        {
            State = state;
        }

        public IProcessState<TEntity> State { get; }
    }
}