//using System.ComponentModel.Composition;
//using SystemInterfaces;
//using CommonMessages;
//using DataInterfaces;
//using ViewModel.Interfaces;

//namespace ViewMessages
//{
//    [Export]
//    public class LoadEntityViewModel<TEntity> : SystemProcessMessage where TEntity : IEntity
//    {
//        public LoadEntityViewModel(IReadEntityViewModelInfo<TEntity> entityViewModelInfo, ISystemProcess process, MessageSource source) : base(process, source)
//        {
//            EntityViewModelInfo = entityViewModelInfo;

//        }

//        public IReadEntityViewModelInfo<TEntity> EntityViewModelInfo { get;}
//    }

//    [Export]
//    public class UnloadEntityViewModel<TEntity> : SystemProcessMessage where TEntity : IEntity
//    {
//        public UnloadEntityViewModel(IReadEntityViewModelInfo<TEntity> entityViewModelInfo, ISystemProcess process, MessageSource source) : base(process,source)
//        {
//            EntityViewModelInfo = entityViewModelInfo;

//        }

//        public IReadEntityViewModelInfo<TEntity> EntityViewModelInfo { get; }
//    }
//}