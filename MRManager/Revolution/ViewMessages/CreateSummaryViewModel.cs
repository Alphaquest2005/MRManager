using System.ComponentModel.Composition;
using SystemInterfaces;
using CommonMessages;
using DataInterfaces;
using ViewModelInterfaces;

namespace ViewMessages
{
    [Export]
    public class CreateSummaryViewModel<T> : SystemProcessMessage, ICreateSummaryViewModel<T>  where T : IEntity
    {
        public CreateSummaryViewModel(ISystemProcess process, MessageSource source) : base(process,source)
        {
        }
    }
}
