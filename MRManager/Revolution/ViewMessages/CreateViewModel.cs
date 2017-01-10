using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;

using ViewModelInterfaces;

namespace ViewMessages
{
    [Export]
    public class CreateViewModel<T> : ProcessSystemMessage, ICreateViewModel<T> where T : IEntity
    {
        public CreateViewModel(IProcessStateInfo processInfo, ISystemProcess process, ISystemSource source) : base(processInfo,process, source)
        {
        }
    }
}
