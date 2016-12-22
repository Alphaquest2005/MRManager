using System.Collections.ObjectModel;
using Core.Common.UI.DataVirtualization;
using DataInterfaces;

namespace Core.Common.UI
{
    public interface IAutoViewModel<T> where T : IEntity
    {
        VirtualList<T> VirtualData { get; set; }
        ObservableCollection<T> SelectedItems { get; set; }
        void ViewAll();
        void SelectAll();

    }
}
