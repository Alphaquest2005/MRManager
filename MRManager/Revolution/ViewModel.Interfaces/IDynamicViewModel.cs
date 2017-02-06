using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Interfaces
{
    
    public interface IDynamicViewModel<out TViewModel> : IViewModel where TViewModel : IViewModel
    {
        TViewModel ViewModel { get; }
    }
}
