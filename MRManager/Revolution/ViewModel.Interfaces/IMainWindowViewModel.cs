using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace ViewModel.Interfaces
{
    
    public interface IMainWindowViewModel : IScreenLayoutViewModel
    {
        ReactiveProperty<IScreenModel> ScreenModel { get; }
    }
}
