using SystemInterfaces;
using CommonMessages;
using DataInterfaces;
using ViewModelInterfaces;

namespace ViewMessages
{
    
    public class CreateViewModel<T> : ProcessSystemMessage, ICreateViewModel<T> where T : IEntity
    {
        public CreateViewModel(ISystemProcess process, ISystemMessage msg) : base(process, msg)
        {
        }
    }
}
