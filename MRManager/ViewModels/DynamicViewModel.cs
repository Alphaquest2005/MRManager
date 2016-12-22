using System.Dynamic;
using Common.Dynamic;

namespace ViewModels
{
    public class DynamicViewModel<T> : Expando 
    {

        public DynamicViewModel(T viewModel) : base(viewModel)
        {

        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            base.InvokeMethod(this, "GetValue", new object[] {binder.Name},out result);
            if(result == null) return base.TryGetMember(binder, out result);
            return true;
        }

    }
}
