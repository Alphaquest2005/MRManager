using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text;
using System.Threading.Tasks;
using SystemInterfaces;
using JB.Collections.Reactive;

namespace ViewModel.Interfaces
{
    
    public interface IAutoViewModel<T> where T : IEntity
    {
       // VirtualList<T> VirtualData { get; set; }
        ObservableCollection<T> SelectedItems { get; set; }
        void ViewAll();
        void SelectAll();

    }
}
