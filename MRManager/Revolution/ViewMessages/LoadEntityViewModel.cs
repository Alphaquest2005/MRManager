//using System.ComponentModel.Composition;
//using SystemInterfaces;
//using CommonMessages;
//
//using ViewModel.Interfaces;

//namespace ViewMessages
//{
//    [Export]
//    public class LoadEntityViewModel<TEntity> : SystemProcessMessage where TEntity : IEntity
//    {
//        public LoadEntityViewModel(IReadEntityViewModelInfo<TEntity> entityViewModelInfo, ISystemProcess process, ISourceMessage msg) : base(process, msg)
//        {
//            EntityViewModelInfo = entityViewModelInfo;

//        }

//        public IReadEntityViewModelInfo<TEntity> EntityViewModelInfo { get;}
//    }

//    [Export]
//    public class UnloadEntityViewModel<TEntity> : SystemProcessMessage where TEntity : IEntity
//    {
//        public UnloadEntityViewModel(IReadEntityViewModelInfo<TEntity> entityViewModelInfo, ISystemProcess process, ISourceMessage msg) : base(process, msg)
//        {
//            EntityViewModelInfo = entityViewModelInfo;

//        }

//        public IReadEntityViewModelInfo<TEntity> EntityViewModelInfo { get; }
//    }
//}