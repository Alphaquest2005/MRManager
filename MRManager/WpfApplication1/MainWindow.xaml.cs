using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Hosting.Extension;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SystemInterfaces;
using CommonMessages;
using Interfaces;
using JB.Collections.Reactive;
using Reactive.Bindings;
using ReactiveUI;
using ViewModel.Interfaces;
using ViewModelInterfaces;


namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var catalog = new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            var genericCatalog = new GenericCatalog(catalog);
            var container = new CompositionContainer(genericCatalog);

            var vtype = typeof (IEntityCacheViewModel<ISyntomPriority>);

            var res = container.GetExportedValueOrDefault<IEntityCacheViewModel<ISyntomPriority>>();
            var res2 = container.GetExportedValueOrDefault<IViewStateLoaded<ISigninViewModel, IProcessState<ISignInInfo>>>();

            object rf =
                container.GetType()
                    .GetMethod("GetExportedValueOrDefault", new Type[] {})
                    .MakeGenericMethod(vtype.GetGenericTypeDefinition().MakeGenericType(vtype.GenericTypeArguments))
                    .Invoke(container, new object[] {});

            var res3 = container.AlphaType(vtype);





        }
    }

    public static class CompositionContainerExtensions
    {
        public static dynamic AlphaType(this CompositionContainer container, Type type)
        {
            var res = container.GetType()
                    .GetMethod("GetExportedValueOrDefault", new Type[] { })
                    .MakeGenericMethod(type.GetGenericTypeDefinition().MakeGenericType(type.GenericTypeArguments))
                    .Invoke(container, new object[] { });
            return res?.GetType();
        }
    }



    [Export(typeof(IViewStateLoaded<,>))]
    public class ViewModeltest<T1, T2> : ProcessSystemMessage, IViewStateLoaded<T1, T2> where T1 : IViewModel where T2 : IProcessState
    {
        public T1 ViewModel { get; }
        public T2 State { get; }
    }


    [Export(typeof(IEntityCacheViewModel<>))]
    public class ViewModel<TEntity> : IEntityCacheViewModel<TEntity> where TEntity : IEntity
    {
        public ISystemSource Source { get; }
        public string ViewName { get; }
        public string ViewSymbol { get; }
        public string ViewDescription { get; }
        public IViewInfo ViewInfo { get; }
        public ISystemProcess Process { get; }
        public List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get; }
        public List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        public Dictionary<string, ReactiveCommand<IViewModel, Unit>> Commands { get; }
        public List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }
        public ObservableBindingList<TEntity> ChangeTrackingList { get; }
        public ObservableList<TEntity> SelectedEntities { get; }
        ReactiveProperty<RowState> IViewModel.RowState { get; }
        public Type Orientation { get; }
        public Type ViewModelType { get; }
        ReactiveProperty<IProcessState<TEntity>> IEntityViewModel<TEntity>.State { get; }
        public ReactiveProperty<TEntity> CurrentEntity { get; }
        public ObservableList<TEntity> EntitySet { get; }
        ReactiveProperty<IProcessStateList<TEntity>> IEntityListViewModel<TEntity>.State { get; }
        public ObservableDictionary<string, dynamic> ChangeTracking { get; }
    }

}
