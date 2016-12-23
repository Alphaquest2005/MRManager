using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dynamic;

namespace Core.Common.UI
{
    public class DynamicViewModel<T> : Expando
    {

        public DynamicViewModel(T viewModel) : base(viewModel)
        {

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

    }
}
