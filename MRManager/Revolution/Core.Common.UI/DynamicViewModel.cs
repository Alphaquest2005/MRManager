using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using Common.Dynamic;

namespace Core.Common.UI
{
    public class DynamicViewModel<TViewModel> : Expando, IViewModel where TViewModel:IViewModel
    {
        private static TViewModel _viewModel;
        public DynamicViewModel(TViewModel viewModel) : base(viewModel)
        {
            _viewModel = viewModel;
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

        public string Name { get; } = _viewModel.Name;
        public string Symbol { get; } = _viewModel.Symbol;
        public string Description { get; } = _viewModel.Description;
        public ISystemProcess Process { get; } = _viewModel.Process;
        public List<IEventSubscription<IViewModel, IEvent>> EventSubscriptions { get; } = _viewModel.EventSubscriptions;
        public List<IEventPublication<IViewModel, IEvent>> EventPublications { get; } = _viewModel.EventPublications;
        public Dictionary<string, dynamic> Commands { get; } = _viewModel.Commands;
        public List<IEventCommand<IViewModel, IEvent>> CommandInfo { get; } = _viewModel.CommandInfo;
    }
}
