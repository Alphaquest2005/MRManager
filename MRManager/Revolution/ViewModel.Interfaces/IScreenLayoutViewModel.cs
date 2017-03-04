using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using JB.Collections.Reactive;


namespace ViewModel.Interfaces
{
    
    public interface IScreenLayoutViewModel: IViewModel
    {
        ObservableList<IViewModel> HeaderViewModels { get; }
        ObservableList<IViewModel> LeftViewModels { get; }
        ObservableList<IViewModel> RightViewModels { get; }
        ObservableList<IViewModel> FooterViewModels { get; }
        ObservableList<IViewModel> BodyViewModels { get;}
        ObservableList<IViewModel> CacheViewModels { get; }
    }
}