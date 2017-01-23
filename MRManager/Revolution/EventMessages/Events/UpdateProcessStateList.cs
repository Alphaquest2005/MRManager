using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    public class UpdateProcessStateList<TEntity> : ProcessSystemMessage, IProcessStateListMessage<TEntity> where TEntity : IEntityId
    {
        public UpdateProcessStateList(IProcessStateList<TEntity> state, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            State = state;
        }

        public IProcessStateList<TEntity> State { get; }
    }
}