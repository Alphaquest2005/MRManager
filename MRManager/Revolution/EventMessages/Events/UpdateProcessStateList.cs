using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

namespace EventMessages.Events
{
    [Export(typeof(IUpdateProcessStateList<>))]
    public class UpdateProcessStateList<TEntity> : ProcessSystemMessage, IUpdateProcessStateList<TEntity> where TEntity : IEntityId
    {
        public UpdateProcessStateList() { }
        public UpdateProcessStateList(IProcessStateList<TEntity> state, IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo, process, source)
        {
            State = state;
        }

        public IProcessStateList<TEntity> State { get; }
    }
}