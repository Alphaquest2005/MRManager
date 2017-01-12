using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

using ViewModelInterfaces;

namespace ViewMessages
{
    [Export]
    public class CreateSummaryViewModel<T> : ProcessSystemMessage, ICreateSummaryViewModel<T>  where T : IEntity
    {
        public CreateSummaryViewModel(IStateCommandInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }
    }
}
