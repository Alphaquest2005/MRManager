using System.ComponentModel.Composition;
using CommonMessages;
using DataInterfaces;
using ViewModelInterfaces;

namespace ViewMessages
{
    [Export]
    public class ViewModelCreated<T> :BaseMessage, IViewModelCreated<T> where T : IEntity
    {
        [ImportingConstructor]
        public ViewModelCreated(IViewModel<T> viewModel, MessageSource source) : base(source)
        {
            ViewModel = viewModel;
        }

        public IViewModel<T> ViewModel { get; }
    }
}
