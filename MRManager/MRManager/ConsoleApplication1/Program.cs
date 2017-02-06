using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Hosting.Extension;
using System.Reflection;
using SystemInterfaces;
using Interfaces;
using Reactive.Bindings;
using ReactiveUI;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Startup().CatalogSetup();
        }
    }

    class Startup
    {
        public void CatalogSetup()
        {
            var catalog = new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            var genericCatalog = new GenericCatalog(catalog);
            var container = new CompositionContainer(genericCatalog);

            var res = container.GetExportedValueOrDefault<IEntityCacheViewModel<IVisitType>>();


        }
    }



   

    [Export(typeof(IEntityCacheViewModel<>))]
    public class ViewModel<TEntity> : IEntityCacheViewModel<TEntity> where TEntity : IEntity
    {
        public ISystemSource Source { get; }
        public string Name { get; }
        public string Symbol { get; }
        public string Description { get; }
        public ISystemProcess Process { get; }
        public List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get; }
        public List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        ConcurrentDictionary IViewModel.Commands { get; }
        public ConcurrentDictionary<string, ReactiveCommand<IViewModel, Unit>> Commands { get; }

        public List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }
        public ReactiveProperty<RowState> RowState { get; }
        public Type Orientation { get; }
        public Type ViewModelType { get; }
        ReactiveProperty<IProcessState<TEntity>> IEntityViewModel<TEntity>.State { get; }
        public ConcurrentDictionary<string, dynamic> ChangeTracking { get; }
        public ReactiveProperty<TEntity> CurrentEntity { get; }
        public ObservableCollection<TEntity> EntitySet { get; }
        public ObservableCollection<TEntity> SelectedEntities { get; }

        ReactiveProperty<IProcessStateList<TEntity>> IEntityListViewModel<TEntity>.State { get; }
      
    }

   
}
