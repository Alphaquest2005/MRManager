using System.ComponentModel.Composition;
using CommonMessages;
using DataInterfaces;
using ViewModelInterfaces;

namespace ViewMessages
{
    [Export]
    public class CreateSummaryViewModel<T> :BaseMessage, ICreateSummaryViewModel<T>  where T : IEntity
    {
        public CreateSummaryViewModel(MessageSource source) : base(source)
        {
        }
    }
}
