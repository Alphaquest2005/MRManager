

using System.ComponentModel.Composition;

namespace SystemInterfaces
{
    [InheritedExport]
    public interface IProcess<out TEntity>:IProcess where TEntity : IEntity
    {
        TEntity Entity { get; }
    }
}