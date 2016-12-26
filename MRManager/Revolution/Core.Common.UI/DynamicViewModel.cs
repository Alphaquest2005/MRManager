using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Common.Dynamic;
using ViewModel.Interfaces;

namespace Core.Common.UI
{
    public class DynamicViewModel<TViewModel> : Expando, IViewModel where TViewModel:IViewModel
    {
        public new TViewModel Instance { get; }
        public DynamicViewModel(TViewModel viewModel) : base(viewModel)
        {
            Contract.Requires(viewModel != null);
            Name = viewModel.Name;
            CommandInfo = viewModel.CommandInfo;
            Commands = viewModel.Commands;
            EventPublications = viewModel.EventPublications;
            EventSubscriptions = viewModel.EventSubscriptions;
            Process = viewModel.Process;
            Description = viewModel.Description;
            Symbol = viewModel.Symbol;
            Instance = (TViewModel) base.Instance;
            MsgSource = ((IViewModel) base.Instance).MsgSource;
        }

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
        public IMessageSource MsgSource { get; }
        public List<IViewModelEventSubscription<IViewModel, IEvent>> EventSubscriptions { get; }
        public List<IViewModelEventPublication<IViewModel, IEvent>> EventPublications { get; }
        public Dictionary<string, dynamic> Commands { get; }
        public List<IViewModelEventCommand<IViewModel, IEvent>> CommandInfo { get; }
    }
}
