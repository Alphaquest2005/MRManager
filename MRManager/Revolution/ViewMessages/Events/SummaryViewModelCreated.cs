//using System.ComponentModel.Composition;
//using SystemInterfaces;
//using CommonMessages;
//
//using ViewModelInterfaces;

//namespace ViewMessages
//{
//    
//    public class SummaryViewModelCreated<T> : SystemProcessMessage, ISummaryViewModelCreated<T> where T : IEntity
//    {
//        [ImportingConstructor]
//        public SummaryViewModelCreated(ISummaryViewModel<T> viewModel, ISystemProcess process, ISystemSource msg) : base(process, msg)
//        {
//            ViewModel = viewModel;
//        }


//        public ISummaryViewModel<T> ViewModel { get; }
//    }
//}
