//using System.ComponentModel.Composition;
//using SystemInterfaces;
//using CommonMessages;
//using DataInterfaces;
//using ViewModelInterfaces;

//namespace ViewMessages
//{
//    [Export]
//    public class SummaryViewModelCreated<T> : SystemProcessMessage, ISummaryViewModelCreated<T> where T : IEntity
//    {
//        [ImportingConstructor]
//        public SummaryViewModelCreated(ISummaryViewModel<T> viewModel, ISystemProcess process, MessageSource source) : base(process,source)
//        {
//            ViewModel = viewModel;
//        }


//        public ISummaryViewModel<T> ViewModel { get; }
//    }
//}
