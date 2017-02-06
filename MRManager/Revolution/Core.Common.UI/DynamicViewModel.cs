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

namespace Core.Common.UI
{


    public class DynamicViewModel<TViewModel> : Expando, IDynamicViewModel<TViewModel> where TViewModel:IViewModel
    {
        public DynamicViewModel() { }
        public ISystemSource Source => new Source(Guid.NewGuid(), "DynamicViewModel:" + typeof(TViewModel).GetFriendlyName(),new SourceType(typeof(DynamicViewModel<TViewModel>)), new MachineInfo(Environment.MachineName, Environment.ProcessorCount));
        public TViewModel ViewModel { get; }

        protected static DynamicViewModel<TViewModel> _instance = null;
        public new static DynamicViewModel<TViewModel> Instance => _instance;
        public DynamicViewModel(TViewModel viewModel) : base(viewModel)
        {
            Contract.Requires(viewModel != null);
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            Name = viewModel.Name;
            CommandInfo = viewModel.CommandInfo;
            Commands = viewModel.Commands;
            EventPublications = viewModel.EventPublications;
            EventSubscriptions = viewModel.EventSubscriptions;
            Process = viewModel.Process;
            Description = viewModel.Description;
            Symbol = viewModel.Symbol;
            ViewModel = (TViewModel) base.Instance;
            Orientation = viewModel.Orientation;
            ViewModelType = typeof (TViewModel);
            RowState = viewModel.RowState;
            _instance = this;
            

        }

        public ReactiveProperty<SystemInterfaces.RowState> RowState { get; }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            base.InvokeMethod(this, "GetValue", new object[] { binder.Name }, out result);
            if (result == null) return base.TryGetMember(binder, out result);
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            object result = null;
            base.InvokeMethod(this, "SetValue", new object[] { value, binder.Name }, out result);
            if (result == null) return base.TrySetMember(binder, value);
            return true;
        }

        public string Name { get; } 
        public string Symbol { get; }
        public string Description { get; }
        public ISystemProcess Process { get; }
        public List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get; }
        public List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        public Dictionary<string, ReactiveCommand<IViewModel, Unit>> Commands { get; }
        public List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }
        public Type Orientation { get; }
        public Type ViewModelType { get; }
    }
}
