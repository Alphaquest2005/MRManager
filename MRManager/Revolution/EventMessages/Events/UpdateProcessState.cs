using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IProcessStateMessage<>))]
    public class UpdateProcessState<TEntity> : ProcessSystemMessage, IProcessStateMessage<TEntity> where TEntity : IEntityId
    {
        public UpdateProcessState() { }
        public UpdateProcessState(IProcessState<TEntity> state, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
            State = state;
        }

        public IProcessState<TEntity> State { get; }
    }
}