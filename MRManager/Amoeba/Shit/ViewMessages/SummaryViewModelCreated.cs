using System.ComponentModel.Composition;
using CommonMessages;
using DataInterfaces;
using ViewModelInterfaces;

namespace ViewMessages
{
    [Export]
    public class SummaryViewModelCreated<T> :BaseMessage, ISummaryViewModelCreated<T> where T : IEntity
    {
        [ImportingConstructor]
        public SummaryViewModelCreated(ISummaryViewModel<T> viewModel, MessageSource source) : base(source)
        {
            ViewModel = viewModel;
        }


        public ISummaryViewModel<T> ViewModel { get; }
    }
}
