//using System;
//using System.Collections.Generic;
//using System.Windows.Input;
//
//using JB.Collections.Reactive;
//using ReactiveUI;
//using ViewModelInterfaces;

//namespace ViewModel.Interfaces
//{
//    public interface IWriteEntityViewModelInfo<out T>: IReadEntityViewModelInfo<T> where T : IEntity
//    {
//        Func<T> CreateEntityAction { get; }
//        Func<T> CreateNullEntityAction { get; }
       
//    }

//    public interface IReadEntityViewModelInfo<out T> : IViewModelInfo where T : IEntity
//    {
//       T EntityType { get; }
//    }

//    public interface IWriteEntityViewModel<T> : IReadEntityViewModel<T> where T : IEntity
//    {
//        T CurrentEntityWithChanges { get; set; }
//        ObservableDictionary<string,dynamic> ChangeTracking { get; }
//        ReactiveCommand NewEntity { get;  }
//        ReactiveCommand DeleteEntity { get;  }
//        ReactiveCommand SaveChanges { get;  }

//        ReactiveCommand EditEntity { get; }

//    }

//    public interface IReadEntityViewModel<T> : IEntityViewModel<T> where T : IEntity
//    {
//        T CurrentEntity { get; }
//        ObservableList<T> EntitySet { get; }
//        ObservableList<T> SelectedEntities { get; }
//    }
//}