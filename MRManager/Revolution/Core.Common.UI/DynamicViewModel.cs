using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Reactive;
using SystemInterfaces;
using Common;
using Common.Dynamic;
using Reactive.Bindings;
using ReactiveUI;
using RevolutionEntities.Process;
using Utilities;
using ViewModel.Interfaces;
using ViewModelInterfaces;

namespace Core.Common.UI
{


    public class DynamicViewModel<TViewModel> : Expando, IDynamicViewModel<TViewModel> where TViewModel:IViewModel
    {
        public DynamicViewModel(){}

        public ISystemSource Source { get; }
        public TViewModel ViewModel { get; }

        protected static DynamicViewModel<TViewModel> _instance = null;
        public new static DynamicViewModel<TViewModel> Instance => _instance;

        
        public DynamicViewModel(TViewModel viewModel) : base(viewModel)
        {
            ViewInfo = viewModel.ViewInfo;
            Contract.Requires(viewModel != null);
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Source = new Source(Guid.NewGuid(), "DynamicViewModel:" + typeof(TViewModel).GetFriendlyName(), new SourceType(typeof(DynamicViewModel<TViewModel>)),viewModel.Process,viewModel.Process.MachineInfo);
            CommandInfo = viewModel.CommandInfo;
            Commands = viewModel.Commands;
            EventPublications = viewModel.EventPublications;
            EventSubscriptions = viewModel.EventSubscriptions;
            Process = viewModel.Process;
            ViewModel = (TViewModel) base.Instance;
            Orientation = viewModel.Orientation;
            ViewModelType = typeof (TViewModel);
            RowState = viewModel.RowState;
            Priority = viewModel.Priority;

            if (_instance == null) _instance = this;

        }

        public ReactiveProperty<SystemInterfaces.RowState> RowState { get; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            base.InvokeMethod(this, "GetValue", new object[] { binder.Name }, out result);
            if (result != null) return true;
            var res = base.TryGetMember(binder, out result);
            if(res == false) throw new InvalidOperationException($"Property not found{binder.Name}");
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            object result = null;
            base.InvokeMethod(this, "SetValue", new object[] { value, binder.Name }, out result);
            if (result != null) return true;
            var res = base.TrySetMember(binder, value);
            if(res == false) throw new InvalidOperationException($"Property not found{binder.Name}");
            return true;
        }

        public void NotifyPropertyChanged(string property)
        {
            this.OnPropertyChanged(property);
        }

        public string ViewName { get; } 
        public string ViewSymbol { get; }
        public string ViewDescription { get; }
        public IViewInfo ViewInfo { get; }
        public ISystemProcess Process { get; }
        public List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get; }
        public List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        public Dictionary<string, ReactiveCommand<IViewModel, Unit>> Commands { get; }
        public List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }
        public Type Orientation { get; }
        public Type ViewModelType { get; }
        public int Priority { get; }
    }
}
