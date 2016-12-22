using CommonMessages;
using DataInterfaces;
using ViewModelInterfaces;

namespace ViewMessages
{
    
    public class CreateViewModel<T> : BaseMessage, ICreateViewModel<T> where T : IEntity
    {
        public CreateViewModel(MessageSource source) : base(source)
        {
        }
    }
}
