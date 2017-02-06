using System.Collections.Generic;
using System.ComponentModel.Composition;
using SystemInterfaces;
using JB.Collections.Reactive;


namespace ViewModel.Interfaces
{
    
    public interface IScreenLayoutViewModel: IViewModel
    {
        ObservableCollection<IViewModel> HeaderViewModels { get; }
        ObservableCollection<IViewModel> LeftViewModels { get; }
        ObservableCollection<IViewModel> RightViewModels { get; }
        ObservableCollection<IViewModel> FooterViewModels { get; }
        ObservableCollection<IViewModel> BodyViewModels { get;}
        ObservableCollection<IViewModel> CacheViewModels { get; }
    }
}