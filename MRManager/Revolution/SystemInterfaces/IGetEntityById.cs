using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    
    [InheritedExport]
    public interface IGetEntityById<T>
    {
        void Create(int entityId);
    }
}