using System.ComponentModel;

namespace Core.Common.UI.DataVirtualization
{
    public interface IVirtualListLoader<T>
    {
        bool CanSort { get; }
        
        void GetLoadRange(int startIndex, int count, SortDescriptionCollection sortDescriptions);
    }
}
